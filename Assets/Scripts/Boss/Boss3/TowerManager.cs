using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
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
    public float maxDistance = 10000f;
    public bool placeMode = true;
    public Tower towerSc;
    public GameObject pickedTower;
    public List<GameObject> placeableList = new();

    public List<GameObject> PlayerTowers = new();
    public Input pInput;

    void Start()
    {
        //        if (targetLayer == 0)
        //           targetLayer = LayerMask.GetMask("TowerPlacements");
        //      placeMode = true;
    }

    public void OnAttack()
    {
        PlaceTower();
        Debug.Log("PlaceTower() was called by PlayerInput!");
    }

    public void PlaceTower()
    {
        GameObject hit;
        if (placeMode)
        {
            // Check raycast for any valid place points
            if (CheckPlacePos(out hit))
            {
                Debug.Log("Hit This Bitch: " + hit);
                if (placeableList.Contains(hit))
                {
                    placeableList.Remove(hit);
                }
                CreateTower();
            }
        }
    }

    public bool CheckPlacePos(out GameObject hitObject)
    {
        // Raycast to check mouse position, if it is hitting
        // any 'block' in the valid layer, for ability to place tower
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * maxDistance * 10f, Color.green, 15f);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance * 10f, targetLayer))
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

    public void CreateTower()
    {
        Debug.Log("Create Tower");
        PlayerTowers.Add(Instantiate(pickedTower, parent: this.GetComponent<Transform>()));
    }
}
