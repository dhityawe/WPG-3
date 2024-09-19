using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HookShotState : IPlayerState
{
    public void EnterState(PlayerStateManager player)
    {
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
        else if (Input.GetKeyDown(KeyCode.R)) // Example: Switch to Reload state
        {
            player.SwitchState(player.reloadState);
        }
    }

    public void ExitState(PlayerStateManager player)
    {
        Debug.Log("Exiting Hook Shot State");
    }
}
