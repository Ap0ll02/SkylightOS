using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class MinigameWire : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Rigidbody2D rb2D;

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
        rb2D = GetComponent<Rigidbody2D>();

        // Set Rigidbody2D to Kinematic to control movement via script
        //rb2D.bodyType = Rigidbody2D.BodyType.Kinematic;

        // Find the LineRenderer component
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    private void Start()
    {
        StartCoroutine(SetInitialPositionAfterLayout());
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && isDraggable)
        {
            // Set the initial position of the LineRenderer  
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, new Vector3(initialPosition.x, initialPosition.y, 0));
            lineRenderer.SetPosition(1, new Vector3(rectTransform.position.x, rectTransform.position.y, 0));

            // Capture the initial mouse position  
            MouseDragStartPos = Input.mousePosition;
            offset = rectTransform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && isDraggable)
        {
            // Update the wire's position based on the mouse position and the offset
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)) + (Vector3)offset;
            newPosition = ClampToDragArea(newPosition);
            rectTransform.position = newPosition;

            // Update the end position of the LineRenderer
            lineRenderer.SetPosition(1, rectTransform.position);

            // Rotate the wire to match the direction of the LineRenderer
            Vector3 direction = rectTransform.anchoredPosition - (Vector2)initialPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rectTransform.rotation = Quaternion.Euler(0, 0, angle - 90); // Adjust the angle by subtracting 90 degrees
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Handle end drag logic if needed
    }

    private Vector2 ClampToDragArea(Vector2 position)
    {
        Vector3[] corners = new Vector3[4];
        dragAreaRectTransform.GetWorldCorners(corners);

        float minX = corners[0].x;
        float maxX = corners[2].x;
        float minY = corners[0].y;
        float maxY = corners[2].y;

        float clampedX = Mathf.Clamp(position.x, minX, maxX);
        float clampedY = Mathf.Clamp(position.y, minY, maxY);

        return new Vector2(clampedX, clampedY);
    }

    private void SetWireColor()
    {
        Color color;

        switch (wireColor)
        {
            case WireColors.Red:
                color = Color.red;
                break;
            case WireColors.Green:
                color = Color.green;
                break;
            case WireColors.Blue:
                color = Color.blue;
                break;
            case WireColors.Yellow:
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

    private Vector3 initialOffset;

    private IEnumerator SetInitialPositionAfterLayout()
    {
        // Wait for the end of the frame to ensure the layout has been updated
        yield return new WaitForEndOfFrame();

        // Set the initial position of the wire in local space
        initialPosition = rectTransform.position;
        SetWireColor();

        // Calculate the initial offset relative to the parent's parent
        RectTransform parentParentRectTransform = rectTransform.parent.parent.GetComponent<RectTransform>();
        initialOffset = initialPosition - parentParentRectTransform.position;

        // Set the initial position of the LineRenderer
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(initialPosition.x, initialPosition.y, 0));
        lineRenderer.SetPosition(1, new Vector3(rectTransform.position.x, rectTransform.position.y, 0));

        // Start the UpdateLineRenderer coroutine
        StartCoroutine(UpdateLineRenderer());
    }


    private IEnumerator UpdateLineRenderer()
    {
        RectTransform parentParentRectTransform = rectTransform.parent.parent.GetComponent<RectTransform>();

        while (true)
        {
            // Update the start position of the LineRenderer to match the initial offset relative to the parent's parent
            if (lineRenderer.positionCount > 0)
            {
                Vector3 parentParentPosition = parentParentRectTransform.position;
                lineRenderer.SetPosition(0, new Vector3(parentParentPosition.x + initialOffset.x, parentParentPosition.y + initialOffset.y, 0));
            }

            // Update the end position of the LineRenderer to match the current position of the wire
            if (lineRenderer.positionCount > 1)
            {
                lineRenderer.SetPosition(1, new Vector3(rectTransform.position.x, rectTransform.position.y, 0));
            }

            yield return null; // Wait for the next frame
        }
    }

}
