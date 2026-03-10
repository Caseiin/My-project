using UnityEngine;

public class EnemyDeathState : BaseState
{
    EnemyController _enemy;
    public EnemyDeathState(EnemyController enemy) : base(enemy)
    {
        _enemy = enemy;
    }
}
