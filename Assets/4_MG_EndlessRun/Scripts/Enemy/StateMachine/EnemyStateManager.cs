using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    IEnemyState currentState;
    
    // Actions states
    public EnemyAttackState enemyAttackState = new EnemyAttackState();

    private void Start()
    {
        // Start with move as the default state
        currentState = enemyAttackState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(IEnemyState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
}
