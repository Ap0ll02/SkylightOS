using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// Garrett Sharp
/// Lockdown canvas functionality
/// </summary>
public class LockdownCanvas : MonoBehaviour
{
    public LoadingScript loadingScript; // Reference to LoadingScript

    public bool isComplete; // Flag to check if loading is complete

    public CanvasGroup canvasGroup; // Reference to the canvas group
    public void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CloseCanvas();
    }

    // Check if the loading bar is complete
    private IEnumerator CheckLoadingComplete()
    {
        while (!loadingScript.isLoaded)
        {
            yield return new WaitForSeconds(0.5f); // Check every 0.5 seconds
        }
        // Loading is complete
        Debug.Log("Loading complete.");
        isComplete = true;
    }

    // For when close
    public void ResetLoading()
    {
        isComplete = false;
        loadingScript.Reset();
    }

    // Open the canvas
    public void OpenCanvas()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        loadingScript.StartLoading();
        StartCoroutine(CheckLoadingComplete());
    }

    // Close the canvas
    public void CloseCanvas()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        ResetLoading();
    }
}
