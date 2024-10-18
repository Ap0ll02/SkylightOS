using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasicWindow : MonoBehaviour, IDragHandler
{
    // The canvas for the window
    public Canvas canvas;

    // Transform for the window
    private RectTransform rectTransform;

    // Button to close
    public Button closeButton;

    // Any other button
    public Button okButton;

    // Start is called before the first frame update
    void Start()
    {
        closeButton.onClick.AddListener(Close);
        //okButton.onClick.AddListener(OnOkClicked);
        rectTransform = GetComponent<RectTransform>();
        Open();
    }

    void IDragHandler.OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Debug.Log("Dragging!!!");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        Debug.Log("Close Clicked!");
        gameObject.SetActive(false);
    }

    void OnOkClicked()
    {
        Debug.Log("Ok Clicked!");
    }
}
