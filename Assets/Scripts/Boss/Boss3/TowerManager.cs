using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void PlaceTower()
    {
        if (placeMode)
        {
            // Check raycast for any valid place points
            if (CheckPlacePos(out var hit))
            {
                Debug.Log("Hit This Bitch: " + hit);
                CreateTower();
            }
        }
    }

    public bool CheckPlacePos(out var hitObject)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, targetLayer))
        {
            Debug.Log("Success");
            return true;
        }
        else
            return false;
    }

    public void CreateTower()
    {
        Debug.Log("Create Tower");
    }
}
