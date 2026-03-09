using UnityEngine;

public class EnemyAttackState : BaseState
{
    EnemyController _enemy;

    public EnemyAttackState(EnemyController enemy) : base(enemy)
    {
        _enemy = enemy;
    }

    public override void OnEnter()
    {
        Debug.Log("Enemy is attacking");
    }
}
