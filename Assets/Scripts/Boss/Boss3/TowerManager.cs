using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// @author: Jack Ratermann
/// @brief: This is the managing class for towers
/// handling placement, spawning, upgrading, etc.

public class TowerManager : MonoBehaviour
{
    // Tower Management: Placing towers, making sure money/payment
    // is taken care of upon purchase

    #region Public References
    public List<Tower> towerPrefabs;
    public Camera mainCamera;
    public LayerMask targetLayer;
    public readonly float maxDistance = 10000.0f;
    public bool placeMode = true;
    public List<GameObject> placeableList = new();
    public GameObject parentBlock;
    public List<GameObject> PlayerTowers = new();
    public LayerMask towerLayer;
    public GameObject upgradeUIPrefab;
    public GameObject towerHit;
    public List<TMPro.TMP_Text> towerTexts;
    #endregion Public References
    private bool clicked = false;
    private Player player;
    private Tower pickedTower;

    public void Start()
    {
        // ==========================================
        // Instantiate The Text For Tower Purchase UI
        // ==========================================
        pickedTower = towerPrefabs[0];
        player = FindObjectOfType<Player>().GetComponent<Player>();
        SetDefaultUIText();
    }

    // Left click, or attack keybind callback function
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PlaceTower();
        }
    }

    // Callback Unity Event for Input, checking for which tower to use
    // 1-5 relating to towerprefabs 0-4
    public void OnChooseTower(InputAction.CallbackContext context)
    {
        switch (context.control.name)
        {
            // Fallthrough the cases, as code is identical
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
                pickedTower = towerPrefabs[int.Parse(context.control.name) - 1];
                break;
            default:
                Debug.LogError("Detected Incorrect Input", gameObject);
                break;
        }
        Debug.Log("Selected Tower: " + pickedTower);
    }

    // Prefab Order | Txt Order
    // Basic = 0, 1
    // SlowDown = 1, 3
    // Mage = 2, 2
    // AOE = 3, 0
    // Trapper = 4, 4
    public void ChooseTower(string number)
    {
        List<string> defaults = new()
        {
            "Basic Cost: " + towerPrefabs[0].GetComponent<Tower>().costToUpgrade[0],
            "SlowDown Cost: " + towerPrefabs[1].GetComponent<Tower>().costToUpgrade[0],
            "Mage Cost: " + towerPrefabs[2].GetComponent<Tower>().costToUpgrade[0],
            "AOE Cost: " + towerPrefabs[3].GetComponent<Tower>().costToUpgrade[0],
            "Trapper Cost: " + towerPrefabs[4].GetComponent<Tower>().costToUpgrade[0],
        };
        // ====================================
        // Confirm Whether To Select Or Upgrade
        // ====================================
        int[] mapThing = new int[] { 1, 3, 2, 0, 4 };
        if (!defaults.Contains(towerTexts[mapThing[int.Parse(number)]].text))
        {
            // WHY IS THIS IF STATEMENT USED TIME TO FIND OUT
            Debug.Log("We found a number correctly");
            UpgradeUICallback();
            return;
        }

        // ====================================
        // Select The Desired Tower To Purchase
        // ====================================
        switch (number)
        {
            // Fallthrough the cases, as code is identical
            case "0":
            case "1":
            case "2":
            case "3":
            case "4":
                pickedTower = towerPrefabs[int.Parse(number)];
                break;
            default:
                Debug.LogError("Detected Incorrect Input", gameObject);
                break;
        }
        Debug.Log("Selected Tower: " + pickedTower);
    }

    private GameObject hit;

    // Handles function calls for ensuring tower can place and creating tower
    public void PlaceTower()
    {
        Debug.Log(player.GetCurrency() + " : " + pickedTower.costToUpgrade[0]);

        // Check raycast for any valid place points, using hit
        // to hold the game object of the object in raycast
        if (CheckPlacePos(out hit))
        {
            Debug.Log("Raycast Success: " + hit);
            Debug.Assert(hit != null, "Incorrect Raycast, hit object not detected");
            // ========================
            // Transaction Confirmation
            // ========================
            if (player.GetCurrency() >= pickedTower.costToUpgrade[0])
            {
                Debug.Log("Calling Transacton");
                Transaction();
            }
            else
            {
                return;
            }
            // ==========================
            // Tower Already Placed Here?
            // ==========================
            if (!hit.CompareTag("BadPostTD"))
            {
                CreateTower(hit);
                PlayerTowers[^1].transform.SetParent(GetComponent<Transform>(), true);
            }
            else
            {
                clicked = true;
            }
        }
    }

    private void Transaction(int level = 0)
    {
        player.SetCurrency(player.GetCurrency() - pickedTower.costToUpgrade[level]);
        Debug.Assert(player.GetCurrency() >= 0, "Transaction completed with an invalid balance.");
    }

    public bool CheckPlacePos(out GameObject hitObject)
    {
        // Raycast to check mouse position, if it is hitting
        // any 'block' in the valid layer, for ability to place tower
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // DEBUG: Turn ON if raycasts need to be drawn
        //Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green, 15f);

        // ==================================
        // Raycast One: TowerPlacements Layer
        // ==================================
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance, targetLayer))
        {
            hitObject = hitInfo.collider.gameObject;
            hitObject.layer = LayerMask.NameToLayer("TowerUpgrade");
            return true;
        }
        // ================================
        // Raycast Two: TowerUpgrades Layer
        // ================================
        else if (Physics.Raycast(ray, out RaycastHit hitI, maxDistance, towerLayer))
        {
            hitObject = hitI.collider.gameObject.GetComponentInParent<Tower>().gameObject;

            if (!PlayerTowers.Contains(hitObject))
            {
                clicked = false;
                return false;
            }

            // ===============
            // Upgrade Process
            // ===============
            towerHit = hitObject;
            if (!clicked)
            {
                clicked = true;

                foreach (GameObject t in PlayerTowers)
                {
                    if (t.TryGetComponent(out Tower tf))
                    {
                        tf.Glow(false);
                    }
                }

                SetDefaultUIText();
                towerHit.GetComponent<Tower>().Glow(true);
                PromptUpgrade(towerHit.GetComponent<Tower>().towerType);
            }
            else
            {
                foreach (GameObject t in PlayerTowers)
                {
                    if (t.TryGetComponent(out Tower tf))
                    {
                        tf.Glow(false);
                    }
                }
                SetDefaultUIText();
                clicked = false;
            }
            return false;
        }
        else
        {
            Debug.Log("Raycast: Failed");
            hitObject = null;
            return false;
        }
    }

    // ==============================
    // Change UI To Show Upgrade Text
    // ==============================
    public void PromptUpgrade(Tower.Towers type)
    {
        int level = towerHit.GetComponent<Tower>().level;
        switch (type)
        {
            case Tower.Towers.AOE:
                towerTexts[0].text =
                    "AOE Upgrade"
                    + "\n"
                    + towerHit.GetComponent<Tower>().costToUpgrade[level]
                    + " Points";
                break;

            case Tower.Towers.Basic:
                towerTexts[1].text =
                    "Basic Upgrade"
                    + "\n"
                    + towerHit.GetComponent<Tower>().costToUpgrade[level]
                    + " Points";
                break;

            case Tower.Towers.Mage:
                towerTexts[2].text =
                    "Mage Upgrade"
                    + "\n"
                    + towerHit.GetComponent<Tower>().costToUpgrade[level]
                    + " Points";
                break;

            case Tower.Towers.SlowDown:
                towerTexts[3].text =
                    "SlowDown Upgrade"
                    + "\n"
                    + towerHit.GetComponent<Tower>().costToUpgrade[level]
                    + " Points";
                break;

            case Tower.Towers.Trapper:
                towerTexts[4].text =
                    "Trapper Upgrade"
                    + "\n"
                    + towerHit.GetComponent<Tower>().costToUpgrade[level]
                    + " Points";
                break;
            default:
                break;
        }
    }

    public void UpgradeUICallback()
    {
        Debug.Log("Made It Past The Button Click");
        if (towerHit)
        {
            UpgradeHitTower(towerHit);
            SetDefaultUIText();
        }
    }

    public void SetDefaultUIText()
    {
        // ==========================================
        // Instantiate The Text For Tower Purchase UI
        // ==========================================
        List<Tower.Towers> types = new()
        {
            Tower.Towers.AOE,
            Tower.Towers.Basic,
            Tower.Towers.Mage,
            Tower.Towers.SlowDown,
            Tower.Towers.Trapper,
        };
        pickedTower = towerPrefabs[0];
        player = FindObjectOfType<Player>().GetComponent<Player>();
        int i = 0;

        int[] towerOrder = new int[] { 3, 0, 2, 1, 4 };

        foreach (TMPro.TMP_Text t in towerTexts)
        {
            t.text = types[i].ToString() + " Cost: " + towerPrefabs[towerOrder[i]].costToUpgrade[0];
            i++;
        }
    }

    public void CreateTower(GameObject parentBlock)
    {
        // Creating the tower
        parentBlock.tag = "BadPostTD";
        PlayerTowers.Add(
            Instantiate(pickedTower.gameObject, parent: parentBlock.GetComponent<Transform>())
        );
        Debug.Assert(PlayerTowers.Count != 0, "Incorrect Instantiation of Tower");
        // References for positioning
        Transform pt = PlayerTowers[^1].GetComponent<Transform>();
        MeshRenderer prend = parentBlock.GetComponent<MeshRenderer>();

        // Centering and raising the tower
        pt.position = prend.bounds.center;
        pt.position += new Vector3(0, 11, 0);
        PlayerTowers[^1].layer = LayerMask.NameToLayer("TowerUpgrade");
        Debug.Log("LAYER: " + PlayerTowers[^1].layer);
        PlayerTowers[^1].GetComponent<Tower>().level = 1;
        foreach (GameObject t in PlayerTowers)
        {
            if (t.TryGetComponent(out Tower tf))
            {
                tf.Glow(false);
            }
        }

        SetDefaultUIText();
        clicked = false;
    }

    public void UpgradeHitTower(GameObject tower)
    {
        Debug.Log("Starting Upgrade");

        // ==============================
        // Confirm Amount & Confirm Level
        // ==============================

        int tLevel = tower.GetComponent<Tower>().level;
        if (player.GetCurrency() > tower.GetComponent<Tower>().costToUpgrade[tLevel])
        {
            Debug.Log("MONEY CORRECT");
            if (tLevel > 3)
            {
                return;
            }

            // ------ Transaction and Upgrade ------
            Transaction(tLevel);
            tower.GetComponent<Tower>().level += 1;

            // =================================
            // Attempt Upgrade & Refund If Fails
            // =================================
            if (!tower.GetComponent<Tower>().UpgradeTower())
            {
                player.SetCurrency(
                    player.GetCurrency() + tower.GetComponent<Tower>().costToUpgrade[tLevel]
                );

                tower.GetComponent<Tower>().Glow(false);
            }
        }
        Debug.Log("Attempted to upgrade tower");
        tower.GetComponent<Tower>().Glow(false);
        SetDefaultUIText();
        clicked = false;
    }
}
