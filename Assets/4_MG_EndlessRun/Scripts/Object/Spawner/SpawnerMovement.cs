using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMovement : MonoBehaviour
{
    [Header("Spawner Movement Settings")]
    public float moveSpeed = 10.0f;       // Speed of the spawner
    public float moveCd = 1.0f;           // Cooldown between movements

    public float xBounds = 2.0f;        // Boundaries for X-axis (-2 to 2)
    public float laneDistance = 2.0f;   // Distance between lanes (horizontal)
    public Vector3 targetPosition;      // The position to move toward

    // Discrete Y positions (1 and 3)
    private readonly float[] possibleYPositions = { 1.0f, 3.0f };

    void Start()
    {
        targetPosition = transform.position;  // Set initial target position
        StartCoroutine(MovementRoutine());    // Start the movement routine
    }

    IEnumerator MovementRoutine()
    {
        while (true)
        {
            // Wait for the specified cooldown
            yield return new WaitForSeconds(moveCd);

            // Randomly select a new X position within bounds, snapped to lane distance
            float randomX = Mathf.Round(Random.Range(-xBounds, xBounds) / laneDistance) * laneDistance;

            // Randomly select a new Y position between 1 and 3
            float randomY = possibleYPositions[Random.Range(0, possibleYPositions.Length)];

            // Set the target position with new X and Y values
            targetPosition = new Vector3(randomX, randomY, transform.position.z);
        }
    }

    void Update()
    {
        SmoothMoveToTarget();  // Handle movement towards the target position
    }

    private void SmoothMoveToTarget()
    {
        // Smoothly move the enemy towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
