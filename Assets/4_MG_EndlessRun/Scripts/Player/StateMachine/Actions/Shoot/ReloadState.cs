using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : IPlayerState
{
    private float reloadTime = 2.0f; // Example: 2 seconds to reload
    public void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entered Reload State");
        // Start reloading animation or timer
    }

    public void UpdateState(PlayerStateManager player)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Already reloading");
        }
        else
        {
            player.StartCoroutine(ReloadCoroutine(player));
        }
    }

    public void ExitState(PlayerStateManager player)
    {
        Debug.Log("Exiting Reload State");
    }

    private IEnumerator ReloadCoroutine(PlayerStateManager player)
    {
        yield return new WaitForSeconds(reloadTime);
        player.SwitchState(player.bulletShootState); // Return to Bullet Shoot after reload
    }
}
