using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : BaseState
{
    EnemyController _enemy;
    GoapAgent _goap;

    public EnemyAttackState(EnemyController enemy, GoapAgent goap) : base(enemy)
    {
        _enemy = enemy;
        _goap = goap;
    }

    public override void OnEnter()
    {
        // GOAP will plan KillPlayer → PlayerInAttackRange already true → runs AttackPlayer
        _goap.CurrentGoal = null;
        _goap.CurrentAction = null;
    }

    public override void Update()
    {
        // Driven by GoapAgent.Update() in EnemyController
    }

    public override void OnExit()
    {
        _goap.CurrentAction?.Stop(); // tells AttackActionStrategy to set Complete = true
        _goap.CurrentAction = null;
        _goap.CurrentGoal = null;
    }

}
