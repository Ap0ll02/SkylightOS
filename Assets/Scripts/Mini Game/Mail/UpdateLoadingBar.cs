using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Quinn Contaldi
/**
This is for my update game
 */

/// @Implementation For click to increase progress, you must attach this loading bar object to a button
/// and have it call the 'ClickLoading()' function.
public class UpdateLoadingBar : MonoBehaviour
{
    // Ref to the progress bar and a state variable, as well as a can continue
    // variable in case it needs to stop.
    public Image progBar;

    public UpdateGameScoreManager scoreManager;
    /// @var Change This Variable To Change Speed Of Progress Bar (Default 0.001f)
    // Note: Changed 0.1f -> 0.5f
    readonly double speed = 0.5;

    /// @var clickFillSpeed Changes the amount the bar fills on one click
    /// @var clickModifier Mainly for performance thief, this variable allows a multiplied increase or decrease
    /// in amount filled by a single click.
    /// @var drainSpeed Decrease/Make a bigger negative number to make progress bar actively drain quicker

    bool isLoaded = false;

    /// @brief Start the loading bar, rather than making IEnum public


    /// @brief The loading progress for clickable loading.
    public void ScoreLoading()
    {
        if(scoreManager.score < scoreManager.winScore)
        {
            progBar.fillAmount = scoreManager.score;
        }
        else if (scoreManager.winReached)
        {
            progBar.fillAmount = scoreManager.winScore;
            isLoaded = true;
        }
        if (scoreManager.score <= 0)
        {
            progBar.fillAmount = 0;
            isLoaded = false;
        }
    }
    // The enumerator to fill the progress bar.
    // isLoaded will return true when bar has filled.

    /// @brief Reset the progress bar to 0
    public void Reset()
    {
        progBar.fillAmount = 0;
        isLoaded = false;
    }
}