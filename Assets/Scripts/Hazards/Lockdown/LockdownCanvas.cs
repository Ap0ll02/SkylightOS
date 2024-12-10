using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Garrett Sharp
/// Some of the lockdown canvas functionality
/// </summary>
public class LockdownCanvas : MonoBehaviour
{
    public LoadingScript loadingScript; // Reference to LoadingScript

    public bool isComplete; // Flag to check if loading is complete

    public GameObject canvas; // Reference to the canvas

    // Start is called before the first frame update
    void Start()
    {
        isComplete = false;
        canvas.SetActive(false); // Enable the canvas
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        StartCoroutine(CheckLoadingComplete());
    }

    private IEnumerator CheckLoadingComplete()
    {
        while (!loadingScript.isLoaded)
        {
            yield return new WaitForSeconds(0.5f); // Check every 0.5 seconds
        }
        // Loading is complete, you can add additional actions here if needed
        Debug.Log("Loading complete.");
        isComplete = true;
    }

    public void ResetLoading()
    {
        isComplete = false;
        loadingScript.isLoaded = false;
        loadingScript.Reset();
    }
}
