using UnityEngine;

public class AttackActionStrategy : IActionStrategy
{
    readonly AttackStrategy _attackStrategy;
    readonly EnemyController _enemy;


    public bool CanPerform => !Complete;
    public bool Complete { get; private set; } = false;

    public AttackActionStrategy(AttackStrategy attackStrategy, EnemyController enemy)
    {
        _attackStrategy = Object.Instantiate(attackStrategy);
        _enemy =enemy;
        _attackStrategy.Initialize(enemy);

    }

    public void Start()
    {
        Complete = false;
    }

    public void Update(float deltaTime)
    {
        _attackStrategy.Attack(_enemy);
        _attackStrategy.Tick(deltaTime);
    }

    public void Stop()
    {
        Complete = true;
        _attackStrategy.ResetCount();
    }
}