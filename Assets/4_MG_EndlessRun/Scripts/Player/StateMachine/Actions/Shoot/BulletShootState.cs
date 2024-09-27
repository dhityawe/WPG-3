using UnityEngine;

public class BulletShootState : IPlayerState
{
    public float bulletPullPower;
    public float projectileSpeed;
    
    private PlayerBase playerBase;
    public void EnterState(PlayerStateManager player)
    {
        // Find the PlayerBase component on the player GameObject
        playerBase = player.GetComponent<PlayerBase>();

        Debug.Log("Entered Bullet Shoot State");
        // Initialization code for shooting bullets
    }

    public void UpdateState(PlayerStateManager player)
    {
        // Handle shooting logic here
        if (Input.GetKeyDown(KeyCode.H)) // Example: Switch to HookShot state
        {
            player.SwitchState(player.hookShotState);
        }

        if (playerBase.bulletAmmo < 1)
        {
            player.SwitchState(player.reloadState);
        }
        else if (playerBase.hookShotAmmo < 1)
        {
            player.SwitchState(player.hookShotState);
        }
    }

    public void ExitState(PlayerStateManager player)
    {
        // Cleanup if necessary when exiting the state
        Debug.Log("Exiting Bullet Shoot State");
    }

    void ShootBullet()
    {

    }
}
