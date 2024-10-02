using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

        // Convert movement vector to camera's local space
        movement = cameraTransform.TransformDirection(movement);
        movement.y = 0.0f; // Ensure the player doesn't move vertically

        // Move the player
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}