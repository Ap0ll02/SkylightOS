using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Jack Ratermann
// Loading Script For Progress Bar
// Depends on nothing
// {speed} variable can be changed to change the speed of the progress bar

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
        StartCoroutine(LoadingBarCRT());
    }

    // The enumerator to fill the progress bar.
    private IEnumerator LoadingBarCRT()
    {
        while (progBar.fillAmount < 1 && canContinue)
        {
            // WaitForSeconds time is 144hz, so try not to change this value for smoothness
            // If you need to change the speed of the progress bar, change the speed variable
            yield return new WaitForSeconds(0.007f);
            progBar.fillAmount += speed;
        }
        isLoaded = true;
    }
}