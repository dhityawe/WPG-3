using UnityEngine;

public class BulletOnHit : MonoBehaviour, IDestroyable
{
    private PlayerBase playerBase;  // Reference to PlayerBase

    // Method to set the playerBase reference
    public void SetPlayerBase(PlayerBase player)
    {
        playerBase = player;
    }

    private void Start()
    {
        if (playerBase != null)
        {
            Debug.Log("PlayerBase reference set successfully.");
        }
        else
        {
            Debug.LogError("PlayerBase reference is not set!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandlePlayerCollision(other);
        }
        if (other.CompareTag("Enemy"))
        {
            HandleEnemyCollision(other);
        }
        if (other.CompareTag("Obstacle"))
        {
            HandleObstacleCollision();
        }
        if (other.CompareTag("Boundary"))
        {
            HandleBoundCollision();
        }
    }

    public void DeactivateObject()
    {
        // Set the bullet's parent to the bullet pool
        transform.SetParent(playerBase.bulletPool.transform);
        gameObject.SetActive(false); // Deactivate the bullet
    }

    private void HandlePlayerCollision(Collider other)
    {
        EnemyMovement enemyMovement = other.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            Vector3 newTargetPosition = enemyMovement.targetPosition;
            newTargetPosition.z += playerBase.bulletPullDistance; // Move forward
            enemyMovement.targetPosition = newTargetPosition; // Update position
        }

        Debug.Log("Bullet hit enemy and moved it forward!");
        DeactivateObject(); // Call the DeactivateObject method
    }

    private void HandleEnemyCollision(Collider other)
    {
        EnemyMovement enemyMovement = other.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            Vector3 newTargetPosition = enemyMovement.targetPosition;
            newTargetPosition.z -= playerBase.bulletPullDistance; // Move backward
            enemyMovement.targetPosition = newTargetPosition; // Update position
        }

        Debug.Log("Bullet hit enemy and moved it backward!");
        DeactivateObject(); // Call the DeactivateObject method
    }

    private void HandleObstacleCollision()
    {
        Debug.Log("Bullet hit obstacle!");
        DeactivateObject(); // Call the DeactivateObject method
    }

    private void HandleBoundCollision()
    {
        Debug.Log("Bullet hit Bound!");
        DeactivateObject(); // Call the DeactivateObject method
    }
}
