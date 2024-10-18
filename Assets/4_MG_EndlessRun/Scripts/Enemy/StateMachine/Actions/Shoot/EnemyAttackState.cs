using UnityEngine;

public class EnemyAttackState : MonoBehaviour, IEnemyState
{
    private EnemyBase enemyBase;
    public void EnterState(EnemyStateManager enemy)
    {
        // Get the enemy base component
        enemyBase = enemy.GetComponent<EnemyBase>();

        Debug.Log("Enemy Attack State");
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        
        Debug.Log("Enemy Attack State");
    }

    public void ExitState(EnemyStateManager enemy)
    {
        Debug.Log("Enemy Attack State");
    }

    void ShootHandler()
    {

        Debug.Log("Enemy is shooting");
    }

    void EnemyShooting()
    {
        Debug.Log("Enemy is shooting");
    }
}
