using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWindowPanel : MonoBehaviour
{
    // Canvas group reference
    public CanvasGroup canvasGroup;

    // Get Reference to the canvas group
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Open the noor
    public void OpenPanel()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    //  Close the panel
    public void ClosePanel()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
