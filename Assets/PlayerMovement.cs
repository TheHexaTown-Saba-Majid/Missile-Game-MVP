using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector2 targetPosition; // Position the player should move toward
    public float smoothSpeed = 5f; // Speed of smooth movement

    void Start()
    {
        // Get the RectTransform of the image
        rectTransform = GetComponent<RectTransform>();
        targetPosition = rectTransform.localPosition; // Initialize to the player's current position
    }

    void Update()
    {
        // Check for touch input or mouse click
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            UpdateTargetPosition(touch.position);
        }
        else if (Input.GetMouseButton(0)) // For mouse input (e.g., in the editor)
        {
            UpdateTargetPosition(Input.mousePosition);
        }

        // Smoothly move the player toward the target position
        rectTransform.localPosition = Vector2.Lerp(rectTransform.localPosition, targetPosition, smoothSpeed * Time.deltaTime);
    }

    private void UpdateTargetPosition(Vector2 inputPosition)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            inputPosition,
            Camera.main,
            out Vector2 localPoint
        );

        // Clamp the local position to the boundaries of the parent panel
        Rect panelRect = (rectTransform.parent as RectTransform).rect;
        localPoint.x = Mathf.Clamp(localPoint.x, panelRect.xMin, panelRect.xMax);
        localPoint.y = Mathf.Clamp(localPoint.y, panelRect.yMin, panelRect.yMax);

        // Update the target position for smooth movement
        targetPosition = localPoint;
    }
}
