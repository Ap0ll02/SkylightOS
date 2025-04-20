using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidencePickUp : MonoBehaviour
{
    public BasicWindow window;
    public bool isPickedUp = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the lever's trigger zone
        if (other.CompareTag("Player") && !isPickedUp)
        {
            window.OpenWindow();
            isPickedUp = true;
        }
    }
}
