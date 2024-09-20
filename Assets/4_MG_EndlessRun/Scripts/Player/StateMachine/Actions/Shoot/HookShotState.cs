using UnityEngine;
public class HookShotState : IPlayerState
{
    private PlayerBase playerBase;
    public void EnterState(PlayerStateManager player)
    {
        // Find the PlayerBase component on the player GameObject
        playerBase = player.GetComponent<PlayerBase>();

        Debug.Log("Entered Hook Shot State");
        // Initialization code for the hook shot
    }

    public void UpdateState(PlayerStateManager player)
    {
        // Handle hook shot logic here
        if (Input.GetKeyDown(KeyCode.B)) // Example: Switch to BulletShoot state
        {
            player.SwitchState(player.bulletShootState);
        }
        
        if (playerBase.hookShotAmmo < 1)
        {
            player.SwitchState(player.reloadState);
        }
    }

    public void ExitState(PlayerStateManager player)
    {
        Debug.Log("Exiting Hook Shot State");
    }
}
