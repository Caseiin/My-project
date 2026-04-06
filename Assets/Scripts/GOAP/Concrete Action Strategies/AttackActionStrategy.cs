using UnityEngine;

public class AttackActionStrategy : IActionStrategy
{
    readonly AttackStrategy attackStrategy;
    readonly EnemyController enemy;


    public bool CanPerform => !Complete;
    public bool Complete { get; private set; } = false;

    public AttackActionStrategy(AttackStrategy attackStrategy, EnemyController enemy)
    {
        this.attackStrategy = attackStrategy;
        this.enemy =enemy;
    }

    public void Start()
    {
        Complete = false;
    }

    public void Update(float deltaTime)
    {
        attackStrategy.Attack(enemy);
    }

    public void Stop()
    {
        Complete = true;
    }
}