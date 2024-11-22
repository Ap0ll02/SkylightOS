using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMinigame : MonoBehaviour
{
    /// <summary>
    /// Jack Ratermann
    /// Abstract mini game class
    /// Dependent On Nothing
    /// Have minigames inherit from this for enforcing structure.
    /// </summary>
    public abstract void StartGame();

    public abstract bool CanContinue();
}
