using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static MinigameWire;

public class MinigameSlot : MonoBehaviour
{
    public enum SlotColors
    {
        Red,
        Green,
        Blue,
        Yellow
    }

    public SlotColors slotColor;

    public Image slotImage;

    public bool isConnected = false;

    private void Start()
    {
        SetWireColor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MinigameWire wire = collision.GetComponent<MinigameWire>();
        if (wire != null)
        {
            // Check if the wire color matches the slot color
            if (wire.wireColor.ToString() == slotColor.ToString())
            {
                wire.isDraggable = false;
                isConnected = true;
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

    private void SetWireColor()
    {
        Color color;

        switch (slotColor)
        {
            case (SlotColors.Red):
                color = Color.red;
                break;
            case (SlotColors.Green):
                color = Color.green;
                break;
            case (SlotColors.Blue):
                color = Color.blue;
                break;
            case (SlotColors.Yellow):
                color = Color.yellow;
                break;
            default:
                color = Color.white;
                break;
        }

        slotImage.color = color;
    }
}
