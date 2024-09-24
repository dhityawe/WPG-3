using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : IPlayerState
{
    private float reloadBulletTime = 2.0f; // Example: 2 seconds to reload
    private float reloadHookTime = 3.0f; // Example: 3 seconds to reload
    private PlayerBase playerBase; // Reference to the PlayerBase component

    public void EnterState(PlayerStateManager player)
    {
        // Find the PlayerBase component on the player GameObject
        playerBase = player.GetComponent<PlayerBase>();

        InitializeReloadCdStats();

        Debug.Log("Entered Reload State");
        // Start reloading animation or timer
    }

    public void UpdateState(PlayerStateManager player)
    {
        if (playerBase.bulletAmmo == 0)
        {
        player.StartCoroutine(ReloadBulletCoroutine(player));
        Debug.Log("Reloading");
        }

        if (!playerBase.isHookShotAble)
        {
        player.StartCoroutine(ReloadHookCoroutine(player));
        Debug.Log("Reloading Hook");
        }

    }

    public void ExitState(PlayerStateManager player)
    {
        Debug.Log("Exiting Reload State");
    }

    void InitializeReloadCdStats()
    {
        reloadBulletTime = playerBase.ReloadBulletCd;
        reloadHookTime = playerBase.ReloadHookCd;
    }

    private IEnumerator ReloadBulletCoroutine(PlayerStateManager player)
    {
        yield return new WaitForSeconds(reloadBulletTime);
        player.SwitchState(player.bulletShootState); // Return to Bullet Shoot after reload
    }

    private IEnumerator ReloadHookCoroutine(PlayerStateManager player)
    {
        Debug.Log("Reloading Bullet Coroutine Started"); 
        yield return new WaitForSeconds(reloadHookTime);
        playerBase.isHookShotAble = true;
        player.SwitchState(player.hookShotState); // Return to Hook Shot after reload
    }
}
