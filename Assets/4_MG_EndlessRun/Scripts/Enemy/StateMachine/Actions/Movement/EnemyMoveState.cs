using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : MonoBehaviour, IEnemyState
{
    public float xBounds = 2.0f;      // Boundaries for X-axis (-2 to 2)
    public float minY = 1.0f;         // Minimum Y boundary (1)
    public float maxY = 2.0f;         // Maximum Y boundary (2)
    public float laneDistance = 2.0f; // Distance between lanes (horizontal and vertical)

    private Vector3 targetPosition;   // The position to move towards

    public EnemyBase enemyBase;       // Reference to the EnemyBase script
    

    public void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enemy Move State Enter");
        // Find the EnemyBase component on the enemy GameObject
        enemyBase = enemy.GetComponent<EnemyBase>();

        // Set initial target position to the current enemy position
        targetPosition = enemy.transform.position;
        
        StartCoroutine(MovementRoutine());
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        SmoothMoveToTarget();
        Debug.Log("Enemy Move State Update");
    }

    public void ExitState(EnemyStateManager enemy)
    {
        Debug.Log("Enemy Move State Exit");
    }

    private void SmoothMoveToTarget()
    {
        // Smoothly move towards the target position with speed using Time.deltaTime for consistent movement
        transform.position = Vector3.Lerp(transform.position, targetPosition, enemyBase.enemyMoveSpeed * Time.deltaTime);
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
}
