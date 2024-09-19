using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    IPlayerState currentState;

    public BulletShootState bulletShootState = new BulletShootState();
    public HookShotState hookShotState = new HookShotState();
    public ReloadState reloadState = new ReloadState();

    private void Start()
    {
        // Start with bullet shoot as the default state
        currentState = bulletShootState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(IPlayerState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
}
