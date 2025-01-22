using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
/// <summary>
/// Garrett Sharp
/// The functionality for a basic window
/// Currently has flags for drag, closable, etc
/// </summary>
public class BasicWindow : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    // Enable/Disable dragging for the window. Set in inherited class or editor if you want the window to always stay at the spawn.
    public bool isDraggable = true;

    // Enable/Disable closing for the window. Set in inherited class or editor if you want the window to not be closable by the user. 
    public bool isClosable = true;

    // Is the window open or not
    public bool isOpen = true;

    // The start position of the mouse when it starts dragging. 
    private Vector3 MouseDragStartPos;

    // Left mouse button, so that user cant drag with right mouse
    public PointerEventData.InputButton leftMouse;

    // The transform of the actual Window, used for various things
    RectTransform rectTransform;

    // The canvas group, used for setting the window to visible or not
    public CanvasGroup canvasGroup;

    // Actions for window open and close events
    public event Action OnWindowOpen;
    public event Action OnWindowClose;

    // Getting the references
    void Awake()
    {
        // Reference to the windows own canvas group
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = (RectTransform)transform;
    }

    private void OnEnable()
    {
        // Set the window to the top when enabled
        transform.SetAsLastSibling();
    }

    // Used for dragging functionality, required for 'IDragHandler'.
    // Override - Can make a tab undraggable 
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == leftMouse && isDraggable)
        {
            transform.localPosition = Input.mousePosition - MouseDragStartPos;
            TrapToScreen();
        }

    }

    // Used for dragging the window aswell, required for 'IPointerDownHandler'
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == leftMouse && isDraggable)
        {
            MouseDragStartPos = Input.mousePosition - transform.localPosition;

            // Makes the window come to the top when pressed, aka focused
            transform.SetAsLastSibling();
        }

    }

    // Keeps the window in the bounds of the screen when dragged. 
    private void TrapToScreen()
    {
        // Get world-space corners of the RectTransform
        Vector3[] worldCorners = new Vector3[4];
        rectTransform.GetWorldCorners(worldCorners);

        // Convert world-space corners to screen-space coordinates
        Vector3 screenCornerBottomLeft = Camera.main.WorldToScreenPoint(worldCorners[0]);
        Vector3 screenCornerTopRight = Camera.main.WorldToScreenPoint(worldCorners[2]);

        // Get screen boundaries in screen-space coordinates
        float screenLeft = 0;
        float screenRight = Screen.width;
        float screenBottom = 0;
        float screenTop = Screen.height;

        // Calculate position adjustment if out of bounds
        Vector3 position = rectTransform.localPosition;

        // Clamp the left edge of the window
        if (screenCornerBottomLeft.x < screenLeft)
        {
            position.x += screenLeft - screenCornerBottomLeft.x;
        }
        // Clamp the right edge of the window
        if (screenCornerTopRight.x > screenRight)
        {
            position.x -= screenCornerTopRight.x - screenRight;
        }
        // Clamp the bottom edge of the window
        if (screenCornerBottomLeft.y < screenBottom)
        {
            position.y += screenBottom - screenCornerBottomLeft.y;
        }
        // Clamp the top edge of the window
        if (screenCornerTopRight.y > screenTop)
        {
            position.y -= screenCornerTopRight.y - screenTop;
        }

        // Update the RectTransform's position
        rectTransform.localPosition = position;
    }

    // Use this for opening the window
    // Basically call this to open the window, if something else opens the window this also has the functionality to 
    // notify whatever you need that the window has been closed
    public void OpenWindow()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        isOpen = true;
        transform.SetAsLastSibling();
        OnWindowOpen?.Invoke();
    }

    // Use this for closing the window
    // DO NOT CLOSE THE WINDOWS BY SETTING THE GAMEOBJECTS TO INACTIVE
    // Same as openWindow, this will notify whatever you need that the window has been closed
    public void CloseWindow()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        isOpen = false;
        OnWindowClose?.Invoke();
    }

    public void ToggleWindow()
    {
        if (isOpen)
            CloseWindow();
        else
            OpenWindow();
    }
}
