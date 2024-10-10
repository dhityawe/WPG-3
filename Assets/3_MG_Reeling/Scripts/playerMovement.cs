using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform cameraTransform;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Initialize movement vector
        Vector3 movement = Vector3.zero;

        // Check for WASD input
        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
        }

        // Normalize the movement vector to prevent faster diagonal movement
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        // Convert movement vector to camera's local space
        movement = cameraTransform.TransformDirection(movement);
        movement.y = 0.0f; // Ensure the player doesn't move vertically

        // Move the player using Rigidbody.MovePosition
        if (movement.magnitude > 0)
        {
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        }
    }
}