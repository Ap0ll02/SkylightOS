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
    public float maxDistance = 50f;
    public bool placeMode = false;
    public Tower towerSc;
    public GameObject pickedTower;

    public List<GameObject> PlayerTowers = new();

    void Start()
    {
        targetLayer = LayerMask.GetMask("TowerPlacements");
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
                CreateTower();
            }
        }
    }

    public bool CheckPlacePos(out GameObject hitObject)
    {
        // Raycast to check mouse position, if it is hitting
        // any 'block' in the valid layer, for ability to place tower
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, targetLayer))
        {
            Debug.Log("Success");
            hitObject = hit.collider.gameObject;
            return true;
        }
        hitObject = null;
        return false;
    }

    public void CreateTower()
    {
        Debug.Log("Create Tower");
        PlayerTowers.Add(Instantiate(pickedTower, parent: this.GetComponent<Transform>()));
    }
}
