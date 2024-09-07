using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody rb;

    // Initialize the Rigidbody component
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        // Default velocity is zero when no keys are pressed
        Vector3 newVelocity = Vector3.zero;

        // Move the hook smoothly upwards
        if (Input.GetKey(KeyCode.W))
        {
            newVelocity = new Vector3(0, speed, 0);
        }

        // Move the hook smoothly downwards
        if (Input.GetKey(KeyCode.S))
        {
            newVelocity = new Vector3(0, -speed, 0);
        }

        // Apply the new velocity to the Rigidbody
        rb.velocity = newVelocity;
    }
}

