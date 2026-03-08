using UnityEngine;

public class EnemyIdleState : BaseState
{
    EnemyController _enemy;
    public EnemyIdleState(EnemyController enemy) : base(enemy)
    {
        _enemy = enemy;
    }
}
