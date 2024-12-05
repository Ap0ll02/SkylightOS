using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMovement : MonoBehaviour
{
    public float speed = 5f;
    public float heightVariation = 1f;
    private float initialY;
    private bool movingRight = true;
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        initialY = transform.position.y;
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float newX = transform.position.x + (movingRight ? speed : -speed) * Time.deltaTime;
        float newY = initialY + Mathf.Sin(Time.time * speed) * heightVariation;
        transform.position = new Vector3(newX, newY, transform.position.z);

        TrapToScreen();

        // Occasionally change direction
        if (Random.Range(0, 100) < 1)
        {
            movingRight = !movingRight;
        }
    }

    // Keeps the window in the bounds of the screen when moving.
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
            movingRight = true;
        }
        // Clamp the right edge of the window
        if (screenCornerTopRight.x > screenRight)
        {
            position.x -= screenCornerTopRight.x - screenRight;
            movingRight = false;
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
