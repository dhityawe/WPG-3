using UnityEngine;

public class BulletOnHit : MonoBehaviour
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
        if (other.gameObject.CompareTag("Player"))
        {
            EnemyMovement enemyMovement = other.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                // Update the Z position of the targetPosition to pull the enemy smoothly
                Vector3 newTargetPosition = enemyMovement.targetPosition;
                newTargetPosition.z += playerBase.bulletPullDistance; // Move forward by bulletPullDistance (e.g. 1 unit)
                enemyMovement.targetPosition = newTargetPosition; // Update targetPosition for smooth movement
            }

            Debug.Log("Bullet hit enemy and moved it forward!");
            transform.SetParent(playerBase.bulletPool.transform);
            gameObject.SetActive(false);


        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyMovement enemyMovement = other.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                // Update the Z position of the targetPosition to pull the enemy smoothly
                Vector3 newTargetPosition = enemyMovement.targetPosition;
                newTargetPosition.z -= playerBase.bulletPullDistance; // Move forward by bulletPullDistance (e.g. 1 unit)
                enemyMovement.targetPosition = newTargetPosition; // Update targetPosition for smooth movement
            }

            Debug.Log("Bullet hit enemy and moved it backward!");
            transform.SetParent(playerBase.bulletPool.transform);
            gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            // Bullet hit obstacle logic

            Debug.Log("Bullet hit obstacle!");
            transform.SetParent(playerBase.bulletPool.transform);
            gameObject.SetActive(false);

            // Obstacle hit logic
            // transform the obstacle set parent

        }
        
    }



}
