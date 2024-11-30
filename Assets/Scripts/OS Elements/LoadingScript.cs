using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Jack Ratermann
/**
 * @brief Loading Script For Progress Bar, Updates a boolean when bar is full
 * <remarks> Depends on nothing </remarks>
 * <remarks> {speed} variable can be changed to change the speed of the progress bar </remarks>
 */

/// @Implementation For click to increase progress, you must attach this loading bar object to a button
/// and have it call the 'ClickLoading()' function.
public class LoadingScript : MonoBehaviour
{
    // Ref to the progress bar and a state variable, as well as a can continue
    // variable in case it needs to stop.
    public Image progBar;
    public bool isLoaded = false;
    public bool canContinue = true;
    public bool clickToLoad = false;

    /// @var Change This Variable To Change Speed Of Progress Bar (Default 0.001f)
    // Note: Changed 0.1f -> 0.7f
    float speed = 0.1f;

    /// @var clickFillSpeed Changes the amount the bar fills on one click
    /// @var clickModifier Mainly for performance thief, this variable allows a multiplied increase or decrease
    /// in amount filled by a single click.
    /// @var drainSpeed Decrease/Make a bigger negative number to make progress bar actively drain quicker
    float clickFillSpeed = 0.1f;
    float clickModifier = 1f;
    float drainSpeed = 0.0005f;

    public float perthiefTime = 1f;

    /// @brief Start the loading bar, rather than making IEnum public
    public void StartLoading()
    {
        if (!isLoaded && !clickToLoad) 
        {
            StartCoroutine(LoadingBarCRT());
        }
        if(!isLoaded && clickToLoad)
        {
            StartCoroutine(DrainProgress());
        }
    }

    /// @brief The loading progress for clickable loading.
    public void ClickLoading()
    {
        if(progBar.fillAmount < 1 && canContinue)
        {
            progBar.fillAmount += (clickFillSpeed * clickModifier);
        }
        else if(canContinue && progBar.fillAmount > 1)
        {
            progBar.fillAmount = 1;
            isLoaded = true;
        }
    }

    /// @brief Coroutine to slowly drain progress for clickable loading. This adds extra challenge.
    /// @Modify see top of file variables {drainSpeed} to modify rate at which progress idly drains.
    private IEnumerator DrainProgress()
    {
        while(progBar.fillAmount < (1-(2*drainSpeed)))
        {
            yield return new WaitForSeconds(0.007f);
            if(progBar.fillAmount > drainSpeed)
            {
                progBar.fillAmount -= drainSpeed;
            }
        }
        isLoaded = true;
    }

    // The enumerator to fill the progress bar.
    // isLoaded will return true when bar has filled.
    private IEnumerator LoadingBarCRT()
    {
        while (progBar.fillAmount < 1)
        {
            // WaitForSeconds time is 144hz, so try not to change this value for smoothness
            // If you need to change the speed of the progress bar, change the speed variable
            if (canContinue)
            {
                yield return new WaitForSeconds(0.007f);
                progBar.fillAmount += speed * Time.deltaTime * perthiefTime;
            }
            else
            {
                yield return new WaitForSeconds(0.007f);
            }
        }
        isLoaded = true;
    }

    /// @brief Reset the progress bar to 0
    public void Reset()
    {
        progBar.fillAmount = 0;
        isLoaded = false;
    }
}