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

    public List<Tower> towerPrefabs;
    public Camera mainCamera;
    public LayerMask targetLayer;
    public readonly float maxDistance = 10000.0f;
    public bool placeMode = true;
    private Tower pickedTower;
    public List<GameObject> placeableList = new();
    public GameObject parentBlock;
    public List<GameObject> PlayerTowers = new();
    private Player player;
    public LayerMask towerLayer;
    public GameObject upgradeUIPrefab;
    public GameObject towerHit;

    public List<TMPro.TMP_Text> towerTexts;

    public void Start()
    {
        pickedTower = towerPrefabs[0];
        player = FindObjectOfType<Player>().GetComponent<Player>();
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

    public void ChooseTower(string number)
    {
        switch (number)
        {
            // Fallthrough the cases, as code is identical
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
                pickedTower = towerPrefabs[int.Parse(number) - 1];
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
            if (player.GetCurrency() >= pickedTower.costToUpgrade[0])
            {
                Debug.Log("Calling Transacton");
                Transaction();
            }
            else
            {
                return;
            }
            CreateTower(hit);
            PlayerTowers[^1].transform.SetParent(GetComponent<Transform>(), true);
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

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance, targetLayer))
        {
            hitObject = hitInfo.collider.gameObject;
            // Redundancy Check
            //Debug.Assert(
            //   hitObject.layer != LayerMask.NameToLayer("TowerPlacements"),
            //  "BUT THIS IS THE WRONG LAYER YOU MORON HOW DARE YOU COLLIDE!"
            //);
            hitObject.layer = LayerMask.NameToLayer("TowerUpgrade");
            return true;
        }
        else if (Physics.Raycast(ray, out RaycastHit hitI, maxDistance, towerLayer))
        {
            hitObject = hitI.collider.gameObject;

            if (!PlayerTowers.Contains(hitI.collider.gameObject))
            {
                return false;
            }

            towerHit = hitObject;
            towerHit.GetComponent<Tower>().Glow(true);
            PromptUpgrade(towerHit.GetComponent<Tower>().towerType);
            return true;
        }
        else
        {
            Debug.Log("Raycast: Failed");
            hitObject = null;
            return false;
        }
    }

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
        }
    }

    public void UpgradeUICallback()
    {
        UpgradeHitTower(towerHit);
    }

    public void CreateTower(GameObject parentBlock)
    {
        // Creating the tower
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
    }

    public void UpgradeHitTower(GameObject tower)
    {
        Debug.Log("Starting Upgrade");
        if (
            player.GetCurrency()
            > tower.GetComponent<Tower>().costToUpgrade[tower.GetComponent<Tower>().level]
        )
        {
            int tLevel = tower.GetComponent<Tower>().level;
            if (tLevel >= 3)
            {
                return;
            }

            // ------ Transaction and Upgrade ------
            Transaction(tower.GetComponent<Tower>().level);
            tower.GetComponent<Tower>().level += 1;

            if (!tower.GetComponent<Tower>().UpgradeTower())
            {
                player.SetCurrency(
                    player.GetCurrency()
                        + tower.GetComponent<Tower>().costToUpgrade[
                            tower.GetComponent<Tower>().level - 1
                        ]
                );
            }
        }
        Debug.Log("Attempted to upgrade tower");
    }
}
