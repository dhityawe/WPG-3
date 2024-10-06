using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMovement : MonoBehaviour
{
    public float movementSpeed = 20f; // Speed at which this path moves towards the player

    // Update is called once per frame
    void Update()
    {
        // Move the GameObject that this script is attached to
        MovePath();
    }

    void MovePath()
    {
        transform.Translate(Vector3.back * movementSpeed * Time.deltaTime); // Move along -z axis
    }
    
}
