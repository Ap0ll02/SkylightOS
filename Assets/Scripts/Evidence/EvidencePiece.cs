using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Garrett Sharp
// EvidencePiece.cs
// This script handles the evidence pieces in the game.
// It is attached to the evidence pieces in the scene.
// The script will trigger an event when the evidence piece is collected.
// The event can be used to update the evidence manager or any other system that needs to know about the evidence collection.
// It can only be collected once.

public class EvidencePiece : MonoBehaviour
{
    public static event Action EvidenceCollected;
    public bool isCollected = false;

    public void Start()
    {
        BasicWindow window = gameObject.GetComponent<BasicWindow>();
        window.ForceCloseWindow();
    }

    public void TriggerEvidence()
    {
        if(isCollected) return;
        // This function will be called when the player interacts with the evidence piece
        // You can add your logic here to handle the evidence piece
        Debug.Log("Evidence piece triggered: " + gameObject.name);
        EvidenceCollected?.Invoke();
        isCollected = true;
    }
}
