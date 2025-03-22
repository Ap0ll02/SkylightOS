using System;
using System.Collections;
using UnityEngine;

public abstract class AbstractMinigame : MonoBehaviour
{
    /// <summary>
    /// Jack Ratermann
    /// Abstract mini game class
    /// Dependent On Nothing
    /// Have minigames inherit from this for enforcing structure.
    /// </summary>
    public bool CanContinue;

    public bool isStarted;

    public bool isComplete = false;

    //public static event Action OnGameEnd;
    public abstract void StartGame();

    public void TryStartGame()
    {
        if (!isStarted)
        {
            isStarted = true;
            StartGame();
        }
        else
        {
            Debug.LogWarning("Game already started bruh");
        }
    }
}
