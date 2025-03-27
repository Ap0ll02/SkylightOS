using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// @author Jack Ratermann
/// @brief This class is to manage the player state and help with
/// game management, like currency.

public class Player : MonoBehaviour
{
    // Management of the players state and game stuff
    public int currency = 50;
    public TowerManager tm;

    void Start()
    {
        currency = 50;
        Debug.Log("Starting Points: " + currency);
    }
}
