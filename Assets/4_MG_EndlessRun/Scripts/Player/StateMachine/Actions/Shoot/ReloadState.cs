using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : IPlayerState
{
    private float reloadTime = 2.0f; // Example: 2 seconds to reload
    private PlayerBase playerBase; // Reference to the PlayerBase component

    public void EnterState(PlayerStateManager player)
    {
        // Find the PlayerBase component on the player GameObject
        playerBase = player.GetComponent<PlayerBase>();

        Debug.Log("Entered Reload State");
        // Start reloading animation or timer
    }

    public void UpdateState(PlayerStateManager player)
    {
        player.StartCoroutine(ReloadCoroutine(player));
        Debug.Log("Reloading");

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
