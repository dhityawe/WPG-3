using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    IPlayerState currentState;
    
    public PlayerBase playerBase;  // Reference to the PlayerBase component

    // Actions states
    public BulletShootState bulletShootState = new BulletShootState();
    public HookShotState hookShotState = new HookShotState();
    public ReloadState reloadState = new ReloadState();

    private void Awake()
    {
        // Initialize the PlayerBase reference (assuming PlayerBase is on the same object)
        playerBase = GetComponent<PlayerBase>();
    }
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
