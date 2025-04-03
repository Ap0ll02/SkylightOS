using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This will be a single game object for the whole map. All enemies traveling on the map will invoke this script
///
/// </summary>
public class NavigationManager : MonoBehaviour
{
    public GameObject waypointParent;
    private List<GameObject> waypoints = new List<GameObject>();
    public void Awake()
    {
        //Debug.Log("Starting");
        if (waypointParent == null)
        {
            //Debug.Log("null");
        }
        else
        {
            SetWaypointArray(waypointParent);
        }
    }

    public GameObject NextWaypoint(int currentIndex)
    {
        if (currentIndex++ < waypoints.Count)
        {
            //Debug.Log("Next Waypoint " + waypoints[currentIndex].name);
            return waypoints[currentIndex];
        }
        return waypoints[waypoints.Count - 1]; //[ waypoints.Count - 1]
    }

    public void SetWaypointArray(GameObject parent)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            waypoints.Add(parent.transform.GetChild(i).gameObject);
        }
    }
}
