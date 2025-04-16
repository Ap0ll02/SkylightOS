using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Garrett Sharp
// EvidenceManager.cs
// This script manages the evidence pieces in the game.
// It keeps track of the number of evidence pieces collected.
// Then at the end of the level it will add the evidences to the player prefs.

public class EvidenceManager : MonoBehaviour
{
    public int EvidenceCount { get; private set; } = 0;

    public void OnEnable()
    {
        EvidencePiece.EvidenceCollected += HandleEvidenceCollected;
    }

    public void OnDisable()
    {
        EvidencePiece.EvidenceCollected -= HandleEvidenceCollected;
    }

    private void HandleEvidenceCollected()
    {
        EvidenceCount++;
    }
}
