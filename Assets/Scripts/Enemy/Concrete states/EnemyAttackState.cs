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
        _goap.CurrentGoal = _goap.Goals.First(g => g.Name == "KillPlayer");

        // Directly queue
        _goap.ActionPlan = new Queue<AgentAction>();
        _goap.ActionPlan.Enqueue(_attackAction);
        _goap.CurrentAction = null;
    }

    public override void Update()
    {
        if (_goap.CurrentAction == null || _goap.CurrentAction.IsDone())
        {
            if (_goap.ActionPlan.Count == 0) return;
            _goap.CurrentAction = _goap.ActionPlan.Dequeue();
        }

        if (!_goap.CurrentAction.Perform())
        {
            _goap.ActionPlan = GoapPlanner.Plan(_goap, _goap.CurrentGoal);
            _goap.CurrentAction = null;
        }
    }

    public override void OnExit()
    {
        // Clean up attack action so it doesn't persist into other states
        _goap.Actions.RemoveWhere(a => a.Name == "AttackPlayer");
        _goap.CurrentAction = null;
        _goap.ActionPlan.Clear();
    }
}
