using System.Collections;
using UnityEngine;

public class ReloadState : IPlayerState
{
    private float reloadBulletTime = 2.0f;
    private float reloadHookTime = 3.0f;
    private PlayerBase playerBase;

    public void EnterState(PlayerStateManager player)
    {
        playerBase = player.GetComponent<PlayerBase>();
        InitializeReloadCdStats();
        Debug.Log("Entered Reload State");

        // Start reloading bullet or hook, depending on what's needed
        if (playerBase.bulletAmmo == 0)
        {
            player.StartCoroutine(ReloadBulletCoroutine(player));
            Debug.Log("Reloading Bullet");
        }

        if (!playerBase.isHookShotAble)
        {
            player.StartCoroutine(ReloadHookCoroutine(player));
            Debug.Log("Reloading Hook");
        }
    }

    public void UpdateState(PlayerStateManager player)
    {
        // No need to constantly check here, logic is handled in coroutines
    }

    public void ExitState(PlayerStateManager player)
    {
        Debug.Log("Exiting Reload State");
    }

    private void InitializeReloadCdStats()
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
        Debug.Log("Reloading Hook Coroutine Started");
        yield return new WaitForSeconds(reloadHookTime);
        playerBase.isHookShotAble = true;
        Debug.Log("Hook is reloaded, isHookShotAble: " + playerBase.isHookShotAble);
        player.SwitchState(player.hookShotState); // Return to Hook Shot after reload
    }
}
