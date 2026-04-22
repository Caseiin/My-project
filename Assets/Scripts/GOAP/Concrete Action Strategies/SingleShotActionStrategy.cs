using UnityEngine;

public class SingleShotActionStrategy : IActionStrategy
{
    readonly AttackStrategy attackStrategy;
    readonly EnemyController enemy;
    bool _hasFired;

    public bool CanPerform => !Complete;
    public bool Complete => _hasFired;

    public SingleShotActionStrategy(AttackStrategy attack, EnemyController enemy){
        attackStrategy = attack;
        this.enemy = enemy;
    }

    public void Start() => _hasFired = false;

    public void Update(float deltaTime){
        if(!_hasFired){
            attackStrategy.Attack(enemy);
            _hasFired = true;
        }
    }

    public void Stop(){}
}
