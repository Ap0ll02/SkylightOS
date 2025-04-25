using TMPro;
using UnityEngine;

/// @author Jack Ratermann
/// @brief This class is to manage the player state and help with
/// game management, like currency.

public class Player : MonoBehaviour
{
    // Management of the players state and game stuff
    private int currency = 100;
    public TowerManager tm;
    public TMP_Text scoreTxt;

    public void Start()
    {
        Debug.Log("Starting Points: " + currency);
        scoreTxt.text = currency.ToString();
    }

    public int GetCurrency()
    {
        scoreTxt.text = currency.ToString();
        return currency;
    }

    public void SetCurrency(int newAmt)
    {
        currency = newAmt;
        scoreTxt.text = currency.ToString();
    }
}
