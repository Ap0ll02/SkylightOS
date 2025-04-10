using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Garrett Sharp
// File for saving and loading info, just a wrapper for PlayerPrefs
// Made all the variables static so they can be accessed from anywhere :)

public class SaveLoad : MonoBehaviour
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public enum Level
    {
        Level1,
        Level2,
        Level3
    }

    public static void IncrementEvidence()
    {
        int currentEvidence = PlayerPrefs.GetInt("Evidence", 0);
        PlayerPrefs.SetInt("Evidence", currentEvidence + 1);
    }

    public static int GetEvidence()
    {
        return PlayerPrefs.GetInt("Evidence", 0);
    }

    public static void ResetEvidence()
    {
        PlayerPrefs.SetInt("Evidence", 0);
    }

    public static Difficulty GameDifficulty
    {
        get { return (Difficulty)PlayerPrefs.GetInt("GameDifficulty", 0); }
        set { PlayerPrefs.SetInt("GameDifficulty", (int)value); }
    }

    public static Level GameLevel
    {
        get { return (Level)PlayerPrefs.GetInt("GameLevel", 0); }
        set { PlayerPrefs.SetInt("GameLevel", (int)value); }
    }
}
