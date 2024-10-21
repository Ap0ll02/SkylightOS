using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BasicWindow : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    // The start position of the mouse when it starts dragging. 
    Vector3 MouseDragStartPos;

    // Left mouse button, so that user cant drag with right mouse
    public PointerEventData.InputButton leftMouse;

    // The transform of the actual Window, used for various things
    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = (RectTransform)transform;
    }

    // Used for dragging functionality, required for 'DragHandler'.
    // Override - Can make a tab undraggable 
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == leftMouse)
        {
            transform.localPosition = Input.mousePosition - MouseDragStartPos;
            TrapToScreen();
        }

    }

    // Used for dragging the window aswell, makes it work properly somehow
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == leftMouse)
        {
            MouseDragStartPos = Input.mousePosition - transform.localPosition;

            // Makes the window come to the top when pressed, aka focused
            transform.SetAsLastSibling();
        }


    }

    // Used for closing the window.
    // Override - Make the Window Unclosable by user or to make something happen on close
    public void Close()
    {
        gameObject.SetActive(false);
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


}
