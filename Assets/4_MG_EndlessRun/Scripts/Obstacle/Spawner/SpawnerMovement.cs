using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMovement : MonoBehaviour
{
    public float xBounds = 2.0f;      // Boundaries for X-axis (-2 to 2)
    public float minY = 1.0f;         // Minimum Y boundary (1)
    public float maxY = 2.0f;         // Maximum Y boundary (2)
    public float laneDistance = 2.0f; // Distance between lanes (horizontal and vertical)

    public Vector3 targetPosition;   // The position to move towards

    public float moveSpeed = 1.0f;    // The speed at which the spawner moves

    public EnemyBase enemyBase;       // Reference to the EnemyBase script
    

    void Start()
    {
        // Set initial target position to the current enemy position
        targetPosition = transform.position;

        // Start the movement coroutine
        StartCoroutine(MovementRoutine());
    }

    IEnumerator MovementRoutine()
    {
        while (true)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(enemyBase.enemyMoveCd);

            // Randomly choose a new lane in the X-axis within bounds
            float randomX = Mathf.Round(Random.Range(-xBounds, xBounds) / laneDistance) * laneDistance;
            
            // Randomly choose a new lane in the Y-axis within bounds
            float randomY = Mathf.Round(Random.Range(minY, maxY) / 1) * 1;

            // Set the target position to the new random lane position
            targetPosition = new Vector3(randomX, randomY, transform.position.z);
        }
    }

    // immediate movement to the new target position and start the coroutine again
    public void MoveToNewPosition()
    {
        // Randomly choose a new lane in the X-axis within bounds
        float randomX = Mathf.Round(Random.Range(-xBounds, xBounds) / laneDistance) * laneDistance;
        
        // Randomly choose a new lane in the Y-axis within bounds
        float randomY = Mathf.Round(Random.Range(minY, maxY) / 1) * 1;

        // Set the target position to the new random lane position
        targetPosition = new Vector3(randomX, randomY, transform.position.z);
    }

    void Update()
    {
        SmoothMoveToTarget();
    }

    private void SmoothMoveToTarget()
    {
        // Smoothly move towards the target position with speed using Time.deltaTime for consistent movement
        transform.position = Vector3.Lerp(transform.position, targetPosition, enemyBase.enemyMoveSpeed * Time.deltaTime);
    }
}
