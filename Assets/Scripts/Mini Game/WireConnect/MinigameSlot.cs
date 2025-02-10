using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinigameSlot : MonoBehaviour, IDropHandler
{
    public char slotColor;

    public void OnDrop(PointerEventData eventData)
    {
        MinigameWire wire = eventData.pointerDrag.GetComponent<MinigameWire>();
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
