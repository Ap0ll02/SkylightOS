using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinigameSlot : MonoBehaviour
{
    public char slotColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MinigameWire wire = collision.GetComponent<MinigameWire>();
        if (wire != null)
        {
            // Check if the wire color matches the slot color
            if (wire.wireColor == slotColor)
            {
                // Correct connection
                Debug.Log("Correct connection!");
            }
            else
            {
                // Incorrect connection
                Debug.Log("Incorrect connection!");
            }
        }
    }
}
