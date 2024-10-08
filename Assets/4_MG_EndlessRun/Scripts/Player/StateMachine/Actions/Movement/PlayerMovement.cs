using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;  // Increased speed for faster transition
    private float xBounds = 2.0f;     // Boundaries for X-axis (-2 to 2)
    private float minY = 1.0f;        // Minimum Y boundary (1)
    private float maxY = 2.0f;        // Maximum Y boundary (2)
    private float xDistance = 2.0f; // Distance between lanes (horizontal)
    private float yDistance = 1.0f; // Distance between lanes (vertical)

    private Vector3 targetPosition;  // The position to move towards

    private PlayerBase playerBase; // Reference to the PlayerBase component

    void Start()
    {
        playerBase = GetComponent<PlayerBase>();
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
        if (!playerBase.isAnimationRunning)
        {
            // Horizontal lane movement (left/right arrows)
            if (Input.GetKeyDown(KeyCode.LeftArrow) && targetPosition.x > -xBounds)
            {
                targetPosition += new Vector3(-xDistance, 0, 0); // Move left
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && targetPosition.x < xBounds)
            {
                targetPosition += new Vector3(xDistance, 0, 0); // Move right
            }

            // Vertical lane movement (up/down arrows)
            if (Input.GetKeyDown(KeyCode.UpArrow) && targetPosition.y < maxY)
            {
                targetPosition += new Vector3(0, yDistance, 0); // Move up
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && targetPosition.y > minY)
            {
                targetPosition += new Vector3(0, -yDistance, 0); // Move down
            }
        }
        else
        {
            // If the player is not able to use the hook shot, switch to the reload state
            Debug.Log("Still HookShot animation, can't move");
        }
    }

    private void SmoothMoveToTarget()
    {
        // Lerp towards target position while allowing Z-axis to remain unaffected
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetPosition.x, targetPosition.y, transform.position.z), moveSpeed * Time.deltaTime);
    }

}
