using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinigameWire : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public enum WireColors
    {
        Red,
        Green,
        Blue,
        Yellow
    }

    public WireColors wireColor;

    private Vector2 offset;

    public RectTransform dragAreaRectTransform;

    // The start position of the mouse when it starts dragging. 
    private Vector3 MouseDragStartPos;

    // Left mouse button, so that user cant drag with right mouse
    public PointerEventData.InputButton leftMouse;

    public bool isDraggable;

    // Reference to the LineRenderer
    public LineRenderer lineRenderer;

    // 
    public Image plugImage;

    // The initial position of the wire in local space
    private Vector3 initialPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // Find the LineRenderer component
        lineRenderer = GetComponentInChildren<LineRenderer>();

    }

    private void Start()
    {
        StartCoroutine(SetInitialPositionAfterLayout());
    }

    private IEnumerator SetInitialPositionAfterLayout()
    {
        // Wait for the end of the frame to ensure the layout has been updated
        yield return new WaitForEndOfFrame();

        // Set the initial position of the wire in local space
        initialPosition = rectTransform.position;
        Vector3 direction = rectTransform.position - initialPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rectTransform.rotation = Quaternion.Euler(0, 0, angle - 90); // Adjust the angle by subtracting 90 degrees
        SetWireColor();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //canvasGroup.alpha = 0.8f;
        //canvasGroup.blocksRaycasts = false;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(dragAreaRectTransform, eventData.position, eventData.pressEventCamera, out offset);
        //offset = (Vector2)rectTransform.anchoredPosition - offset;

        // Set the initial position of the LineRenderer
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, initialPosition);
        lineRenderer.SetPosition(1, rectTransform.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == leftMouse && isDraggable)
        {
            Vector2 localPoint;
            rectTransform.localPosition = Input.mousePosition - MouseDragStartPos;
            TrapToScreen();
            //if (RectTransformUtility.ScreenPointToLocalPointInRectangle(dragAreaRectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
            //{
                //Vector2 newPosition = localPoint + offset;
                //newPosition.x = Mathf.Clamp(newPosition.x, 0, dragAreaRectTransform.rect.width - rectTransform.rect.width);
                //newPosition.y = Mathf.Clamp(newPosition.y, -dragAreaRectTransform.rect.height + rectTransform.rect.height, 0);
                //rectTransform.anchoredPosition = newPosition; // Use anchoredPosition instead of localPosition  

                // Update the end position of the LineRenderer
                lineRenderer.SetPosition(1, rectTransform.position);

                // Rotate the wire to match the direction of the LineRenderer
                Vector3 direction = rectTransform.position - initialPosition;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rectTransform.rotation = Quaternion.Euler(0, 0, angle - 90); // Adjust the angle by subtracting 90 degrees
            //}
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //canvasGroup.alpha = 1f;
        //canvasGroup.blocksRaycasts = true;
    }

    private void SetWireColor()
    {
        Color color;

        switch (wireColor)
        {
            case (WireColors.Red):
                color = Color.red;
                break;
            case (WireColors.Green):
                color = Color.green;
                break;
            case (WireColors.Blue):
                color = Color.blue;
                break;
            case (WireColors.Yellow):
                color = Color.yellow;
                break;
            default:
                color = Color.white;
                break;
        }

        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        plugImage.color = color;
    }
    private void TrapToScreen()
    {
        // Get world-space corners of the RectTransform
        Vector3[] worldCorners = new Vector3[4];
        rectTransform.GetWorldCorners(worldCorners);

        // Convert world-space corners to screen-space coordinates
        Vector3 screenCornerBottomLeft = Camera.main.WorldToScreenPoint(worldCorners[0]);
        Vector3 screenCornerTopRight = Camera.main.WorldToScreenPoint(worldCorners[2]);

        // Get screen boundaries in screen-space coordinates using dragAreaRectTransform
        Vector3[] dragAreaCorners = new Vector3[4];
        dragAreaRectTransform.GetWorldCorners(dragAreaCorners);
        Vector3 screenLeftBottom = Camera.main.WorldToScreenPoint(dragAreaCorners[0]);
        Vector3 screenRightTop = Camera.main.WorldToScreenPoint(dragAreaCorners[2]);

        float screenLeft = screenLeftBottom.x;
        float screenRight = screenRightTop.x;
        float screenBottom = screenLeftBottom.y;
        float screenTop = screenRightTop.y;

        // Calculate position adjustment if out of bounds
        Vector3 position = rectTransform.localPosition;

        // Clamp the left edge of the wire
        if (screenCornerBottomLeft.x < screenLeft)
        {
            position.x += screenLeft - screenCornerBottomLeft.x;
        }
        // Clamp the right edge of the wire
        if (screenCornerTopRight.x > screenRight)
        {
            position.x -= screenCornerTopRight.x - screenRight;
        }
        // Clamp the bottom edge of the wire
        if (screenCornerBottomLeft.y < screenBottom)
        {
            position.y += screenBottom - screenCornerBottomLeft.y;
        }
        // Clamp the top edge of the wire
        if (screenCornerTopRight.y > screenTop)
        {
            position.y -= screenCornerTopRight.y - screenTop;
        }

        // Update the RectTransform's position
        rectTransform.localPosition = position;
    }


}
