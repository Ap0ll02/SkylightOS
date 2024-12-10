using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NyanCatStrugglingScript : MonoBehaviour
{
    public GameObject Line1Prefab;
    public GameObject Line1;
    public Vector3 Line1Position;
    public bool Line1On = false;
    public GameObject Line2Prefab;
    public GameObject Line2;
    public Vector3 Line2Position;
    public bool Line2On = false;
    public GameObject[] NyanCatItems = new GameObject[40];
    public int index = 0;
    public int itemsCanAccess = 10;
    public GameObject trashCan;
    public GameObject newTrashCan;
    bool isTrashCanSpawned = false;
    // Start is called before the first frame update
    void Awake()
    {
        Line1 = Instantiate(Line1Prefab);
        Line2 = Instantiate(Line2Prefab);
    }

    void Start()
    {
        Line1 = Instantiate(Line1Prefab);
        Line2 = Instantiate(Line2Prefab);
        if (Line1 == null || Line2 == null)
        {
            Debug.LogError("Lazer1 or Lazer2 GameObject is not assigned in the Inspector!");
        }
        var Lazer1transform = Line1.GetComponent<Transform>();
        var Lazer2transform = Line2.GetComponent<Transform>();
        Line1Position = Lazer1transform.position;
        Line2Position = Lazer2transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
        dropItem();
    }

    void FollowMouse()
    {
        // His line retrieves the current position of the mouse cursor in screen space (measured in pixels). The Input.mousePosition property returns a Vector3 
        Vector3 mousePosition = Input.mousePosition;
        // We need to set the z Coordinante since its set to 0 thus we find the closest distance from the camera in which objects can be rendered aka nearClipPlane
        mousePosition.z = Camera.main.nearClipPlane; // Set this to the distance from the camera to the object
        // This line converts the mousePosition from screen space to world space. The ScreenToWorldPoint method takes a Vector3 is screen space and returns a Vector 3 in world space 
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // This just places our Nyan Cat in the correct place 
        transform.position = worldPosition;
    }

    public void dropItem()
    {
        // (index < itemsCanAccess): Check if the current index is less than the number of items that can be accessed.
        // Check if the y-coordinate of the NyanCatStrugglingScript GameObject's position is greater than the y-coordinate of the lazer1 GameObject's position.
        // •(Lazer1On == false): Check if Lazer1On is false (i.e., the first laser is not on).
        if ((index < itemsCanAccess) && (transform.position.y > Line1Position.y) && (Line1On == false))
        {
            var spawnedItem = Instantiate(NyanCatItems[index]);
            spawnedItem.SetActive(true);
            Destroy(spawnedItem,5.0f);
            index++;
            Line1On = true;
            Line2On = false;
        }

        // Since Laser2 is at a negative y thus we need to check if the position is less then this y
        // 
        if ((index < itemsCanAccess) && (transform.position.y < Line2Position.y) && (Line2On == false))
        {
            var spawnedItem = Instantiate(NyanCatItems[index]);
            spawnedItem.SetActive(true);
            Destroy(spawnedItem,5.0f);
            index++;
            Line1On = false;
            Line2On = true;
        }

        if (index == itemsCanAccess)
        {
            Debug.Log("activate trash cat");
            TrashCat();
        }
    }

    public void TrashCat()
    {
        if (isTrashCanSpawned == false)
        {
            isTrashCanSpawned = true;
            newTrashCan = Instantiate(trashCan);
        }
        else
        {

            Debug.Log($"NewTrashCan: {newTrashCan.transform.position}");
            Debug.Log($"NyanCat: {transform.position}");
            // Calculate the distance for x and y coordinates only
            float distanceX = transform.position.x - newTrashCan.transform.position.x;
            float distanceY = transform.position.y - newTrashCan.transform.position.y;
            float distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);
            Debug.Log($"distance: {distance}");
            if (distance <= 2.0f)
            {
                EndGame();
            }
        }
    }

    public void EndGame()
    {
        Destroy(Line1);
        Destroy(Line2);
        Destroy(newTrashCan);
        Destroy(gameObject);
    }
}
