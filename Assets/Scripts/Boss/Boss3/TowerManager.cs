using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float maxDistance = 100000.0f;
    public bool placeMode = true;
    public Tower towerSc;
    public Tower pickedTower;
    public List<GameObject> placeableList = new();
    public GameObject parentBlock;
    public List<GameObject> PlayerTowers = new();
    public Player player;

    void Start()
    {
        //        if (targetLayer == 0)
        //           targetLayer = LayerMask.GetMask("TowerPlacements");
        //      placeMode = true;
        pickedTower = towerPrefabs[0];
    }

    // Left click, or attack keybind callback function
    public void OnAttack()
    {
        PlaceTower();
        Debug.Log("PlaceTower() was called by PlayerInput!");
    }

    // Handles function calls for ensuring tower can place and creating tower
    public void PlaceTower()
    {
        if (player.currency >= pickedTower.costToUpgrade[pickedTower.level])
        {
            Transaction();
        }
        else
            return;

        GameObject hit;
        if (placeMode)
        {
            // Check raycast for any valid place points, using hit
            // to hold the game object of the object in raycast
            if (CheckPlacePos(out hit))
            {
                Debug.Log("Hit This Bitch: " + hit);
                if (placeableList.Contains(hit))
                {
                    placeableList.Remove(hit);
                }
                CreateTower(hit);
                PlayerTowers[^1].transform.SetParent(this.GetComponent<Transform>(), true);
            }
        }
    }

    void Transaction()
    {
        player.currency -= pickedTower.costToUpgrade[pickedTower.level];
        pickedTower.level += 1;
    }

    public bool CheckPlacePos(out GameObject hitObject)
    {
        // Raycast to check mouse position, if it is hitting
        // any 'block' in the valid layer, for ability to place tower
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // DEBUG: Turn ON if raycasts need to be drawn
        // Debug.DrawRay(ray.origin, ray.direction * maxDistance * 100f, Color.green, 15f);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance * 100f, targetLayer))
        {
            hitObject = hitInfo.collider.gameObject;
            return true;
        }
        else
        {
            Debug.Log("Raycast: Failed");
            hitObject = null;
            return false;
        }
    }

    public void CreateTower(GameObject parentBlock)
    {
        // Creating the tower
        Debug.Log("Create Tower");
        PlayerTowers.Add(
            Instantiate(pickedTower.gameObject, parent: parentBlock.GetComponent<Transform>())
        );

        // References for positioning
        Transform pt = PlayerTowers[^1].GetComponent<Transform>();
        MeshRenderer prend = parentBlock.GetComponent<MeshRenderer>();

        // Centering and raising the tower
        pt.position = prend.bounds.center;
        pt.position += new Vector3(0, 11, 0);
    }
}
