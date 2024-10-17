using UnityEngine;
using System.Collections;

public class BulletShootState : IPlayerState
{   
    // Bullet Prefab Object Instantiation
    private GameObject bulletPoolParent;
    private bool isShooting = true;  // Controls shooting availability
    
    private PlayerBase playerBase; 
    private Coroutine bulletShootCoroutine; // To hold the coroutine

    public void EnterState(PlayerStateManager player)
    {
        // Find the PlayerBase component on the player GameObject
        playerBase = player.GetComponent<PlayerBase>();

        Debug.Log("Entered Bullet Shoot State");
        // Initialization code for shooting bullets
    }

    public void UpdateState(PlayerStateManager player)
    {
        // Switch state logic
        if (Input.GetKeyDown(KeyCode.H)) // Example: Switch to HookShot state
        {
            player.SwitchState(player.hookShotState);
        }

        // Handle shooting logic
        if (isShooting && Input.GetKeyDown(KeyCode.Space))
        {
            // Start the shooting process and cooldown
            isShooting = false;
            bulletShootCoroutine = playerBase.StartCoroutine(BulletShootCoroutine()); // Start coroutine
            Debug.Log("Shooting Bullet");
        }

        // Switch to reload state when out of ammo
        if (playerBase.bulletAmmo == 0)
        {
            player.SwitchState(player.reloadState);
        }
    }

    public void ExitState(PlayerStateManager player)
    {
        // Cleanup if necessary when exiting the state
        Debug.Log("Exiting Bullet Shoot State");
    }

    void ShootBullet()
    {
        // Check if there are any bullets left in the pool
        int bulletCount = playerBase.bulletPool.transform.childCount;
        if (bulletCount > 0)
        {
            // Get the last bullet from the pool
            GameObject bullet = playerBase.bulletPool.transform.GetChild(bulletCount - 1).gameObject;

            // Set the bullet position and rotation to match the BulletSpawnPoint
            bullet.transform.position = playerBase.bulletSpawnPoint.position;
            bullet.transform.rotation = playerBase.bulletSpawnPoint.rotation;

            // Deactivate the bullet's parent-child relationship to prevent it from following the player
            bullet.transform.SetParent(null); // Set parent to null so it is no longer a child of the BulletSpawnPoint

            // Activate the bullet
            bullet.SetActive(true);

            // Pass the PlayerBase reference to the BulletOnHit script
            BulletOnHit bulletOnHit = bullet.GetComponent<BulletOnHit>();
            if (bulletOnHit != null)
            {
                bulletOnHit.SetPlayerBase(playerBase);  // Pass the reference of playerBase
            }

            // Apply force to the bullet in the forward direction (using the player's bulletSpeed)
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero; // Reset velocity before applying new force
            rb.AddForce(playerBase.bulletSpawnPoint.forward * playerBase.bulletSpeed, ForceMode.Impulse);

            // Decrease ammo count
            playerBase.bulletAmmo--;
        }
        else
        {
            Debug.Log("No bullets left in the pool");
        }
    }

    // Coroutine to handle shooting cooldown
    IEnumerator BulletShootCoroutine()
    {
        // Call ShootBullet method to fire a bullet
        ShootBullet();
        
        // Wait for the cooldown duration before allowing the player to shoot again
        yield return new WaitForSeconds(playerBase.shootingBulletCd);

        // After cooldown is over, allow shooting again
        isShooting = true;
        Debug.Log("Bullet Shoot Cooldown Finished");
    }
}
