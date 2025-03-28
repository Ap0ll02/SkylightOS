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
        Debug.Log("Starting");
        if (waypointParent == null)
        {
            Debug.Log("null");
        }
        else
        {
            SetWaypointArray(waypointParent);
        }
    }

    public GameObject NextWaypoint(int currentIndex)
    {
        //Debug.Log("Waypoint Count " + waypoints.Count);
        if (currentIndex++ < waypoints.Count)
        {
            Debug.Log("Next Waypoint " + waypoints[currentIndex].name);
            return waypoints[currentIndex];
        }
        throw new System.Exception("No more waypoints");
    }

    public void SetWaypointArray(GameObject parent)
    {
        int index;
        if (parent == null)
        {
            //Debug.Log("No parent");
        }
        else
        {
            index = parent.transform.childCount;
            //Debug.Log(index);
            for (int i = 0; i < index; i++)
            {
                var child = parent.transform.GetChild(i).gameObject;
                if (child == null)
                {
                    //Debug.Log("child null");
                }
                //Debug.Log("in the loop");
                waypoints.Add(parent.transform.GetChild(i).gameObject);
                //Debug.Log(waypoints[i].name);
            }
        }
    }
}
