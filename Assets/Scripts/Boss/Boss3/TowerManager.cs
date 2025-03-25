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
    public bool placeMode = true;
    public Tower towerSc;
    public GameObject pickedTower;
    public List<GameObject> placeableList = new();

    public List<GameObject> PlayerTowers = new();
    public Input pInput;

    void Start()
    {
        if (targetLayer == 0)
            targetLayer = LayerMask.GetMask("TowerPlacements");
        placeMode = true;
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
            Debug.Log("Place Mode On:");
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
        Debug.Log("Place Mode Was False?: " + placeMode);
    }

    public bool CheckPlacePos(out GameObject hitObject)
    {
        Debug.Log("Here we are, raycasting...");
        // Raycast to check mouse position, if it is hitting
        // any 'block' in the valid layer, for ability to place tower
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            Debug.Log(
                $"Raycast hit: {hit.collider.gameObject.name} on layer {LayerMask.LayerToName(hit.collider.gameObject.layer)}"
            );
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
