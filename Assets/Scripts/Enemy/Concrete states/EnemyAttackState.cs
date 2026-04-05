using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : BaseState
{
    EnemyController _enemy;
    GoapAgent _goap;
    AgentAction _attackAction;
    NavMeshAgent _nav;
    public EnemyAttackState(EnemyController enemy, GoapAgent goap): base(enemy)
    {
        _enemy = enemy;
        _goap = goap;
        _nav = goap.NavAgent;
    }

    public override void OnEnter()
    {
        Debug.Log("Enemy is attacking");

        if (_goap.NavAgent.isActiveAndEnabled && _goap.NavAgent.isOnNavMesh)
            _goap.NavAgent.ResetPath();

        _attackAction = new AgentAction("AttackPlayer")
            .WithCost(1f)
            .WithPerformance(() =>
            {
                _enemy.AttackStrategy?.Attack(_enemy);
                return true;
            })
            .WithCompletion(() => !_enemy.AttackSensor.IsTargetInRange);

        _goap.Actions.Add(_attackAction);
        _goap.CurrentAction = _attackAction;  // set directly, no queue needed
    }

    public override void Update()
    {
        if (_goap.CurrentAction == null) return;

        // Action finished — player left range
        if (_goap.CurrentAction.IsDone())
        {
            _goap.CurrentAction = null;
            return;
        }

        // Keep performing every frame — Attack() handles its own cooldown internally
        _goap.CurrentAction.Perform();
    }

    public override void OnExit()
    {
        _goap.Actions.Remove(_attackAction);
        _goap.CurrentAction = null;
    }
}
