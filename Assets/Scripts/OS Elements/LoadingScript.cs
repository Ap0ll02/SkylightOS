using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Jack Ratermann
// Loading Script For Progress Bar, Updates a boolean when bar is full
// Depends on nothing
// {speed} variable can be changed to change the speed of the progress bar

// To implement a progress bar, one simply needs a reference/variable of type LoadingScript, and then call StartLoading() on it.

/**
 * <summary> Loading Script For Progress Bar, Updates a boolean when bar is full </summary>
 * <remarks> Depends on nothing </remarks>
 * <remarks> {speed} variable can be changed to change the speed of the progress bar </remarks>
 */
public class LoadingScript : MonoBehaviour
{
    // Ref to the progress bar and a state variable, as well as a can continue
    // variable in case it needs to stop.
    [SerializeField] Image progBar;
    public bool isLoaded = false;
    public bool canContinue = true;

    // Change This Variable To Change Speed Of Progress Bar (Default 0.001f)
    float speed = 0.001f;

    // Start the loading bar, rather than making IEnum public
    public void StartLoading()
    {
        if (!isLoaded) 
        {
            StartCoroutine(LoadingBarCRT());
        }
        
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
            progBar.fillAmount += speed;
            }
            else
            {
                yield return new WaitForSeconds(0.007f);
            }
        }
        isLoaded = true;
    }
}