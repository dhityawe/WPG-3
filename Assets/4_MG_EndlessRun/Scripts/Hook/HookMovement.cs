using UnityEngine;

public class HookMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;  // Increased speed for faster transition
    public float xBounds = 2.0f;     // Boundaries for X-axis (-2 to 2)
    public float minY = 1.0f;        // Minimum Y boundary (1)
    public float maxY = 2.0f;        // Maximum Y boundary (2)
    public float laneDistance = 2.0f; // Distance between lanes (horizontal and vertical)

    private Vector3 targetPosition;  // The position to move towards

    void Start()
    {
        // Set initial target position to the starting position (x: 0, y: 1)
        targetPosition = new Vector3(0, 1, transform.position.z);
    }

    void Update()
    {
        HandleMovementInput();
        SmoothMoveToTarget();
    }

    private void HandleMovementInput()
    {
        // Horizontal lane movement (left/right arrows)
        if (Input.GetKeyDown(KeyCode.LeftArrow) && targetPosition.x > -xBounds)
        {
            targetPosition += new Vector3(-laneDistance, 0, 0); // Move left
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && targetPosition.x < xBounds)
        {
            targetPosition += new Vector3(laneDistance, 0, 0); // Move right
        }

        // Vertical lane movement (up/down arrows)
        if (Input.GetKeyDown(KeyCode.UpArrow) && targetPosition.y < maxY)
        {
            targetPosition += new Vector3(0, laneDistance, 0); // Move up
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && targetPosition.y > minY)
        {
            targetPosition += new Vector3(0, -laneDistance, 0); // Move down
        }
    }

    private void SmoothMoveToTarget()
    {
        // Increase moveSpeed for faster smooth transition
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
