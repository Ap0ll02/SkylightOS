using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinigameWire : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public char wireColor;
    private Vector2 offset;
    public RectTransform parentRectTransform;

    // The start position of the mouse when it starts dragging. 
    private Vector3 MouseDragStartPos;

    // Left mouse button, so that user cant drag with right mouse
    public PointerEventData.InputButton leftMouse;

    public bool isDraggable;

    // Reference to the LineRenderer
    public LineRenderer lineRenderer;

    // The initial position of the wire in local space
    private Vector3 initialPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        parentRectTransform = transform.parent.parent.GetComponent<RectTransform>();

        // Find the LineRenderer component
        lineRenderer = GetComponentInChildren<LineRenderer>();

        // Store the initial position of the wire in local space
        initialPosition = rectTransform.anchoredPosition;
        Debug.Log("Initial Position (Local): " + initialPosition);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.8f;
        canvasGroup.blocksRaycasts = false;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out offset);
        offset = (Vector2)rectTransform.anchoredPosition - offset;

        // Set the initial position of the LineRenderer
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, rectTransform.position);
        lineRenderer.SetPosition(1, rectTransform.position);
        Debug.Log("OnBeginDrag - Initial Position: " + initialPosition);
        Debug.Log("OnBeginDrag - Current Position: " + rectTransform.anchoredPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == leftMouse && isDraggable)
        {
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
            {
                Vector2 newPosition = localPoint + offset;
                newPosition.x = Mathf.Clamp(newPosition.x, 0, parentRectTransform.rect.width - rectTransform.rect.width);
                newPosition.y = Mathf.Clamp(newPosition.y, -parentRectTransform.rect.height + rectTransform.rect.height, 0);
                rectTransform.anchoredPosition = newPosition; // Use anchoredPosition instead of localPosition  

                // Update the end position of the LineRenderer
                lineRenderer.SetPosition(1, rectTransform.position);
                Debug.Log("OnDrag - New Position: " + newPosition);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Optionally, reset the LineRenderer or keep it as is
        // lineRenderer.positionCount = 0;
        Debug.Log("OnEndDrag - Final Position: " + rectTransform.anchoredPosition);
    }
}
