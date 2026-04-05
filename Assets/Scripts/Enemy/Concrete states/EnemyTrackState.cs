using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTrackState : BaseState
{
    EnemyController _enemy;
    GoapAgent _goap;
    NavMeshAgent _nav;
    AgentAction _trackAction;

    public EnemyTrackState(EnemyController enemy, GoapAgent goap) : base(enemy)
    {
        _enemy = enemy;
        _goap = goap;
        _nav = goap.NavAgent;
    }

    public override void OnEnter()
    {
        Debug.Log("Enemy is tracking");
        _nav.speed = _enemy.MoveSpeed;

        _trackAction = new AgentAction("TrackPlayer")
            .WithCost(1f)
            .WithPerformance(() =>
            {
                if (_enemy.PlayerPosition != null 
                    && _goap.NavAgent.isActiveAndEnabled 
                    && _goap.NavAgent.isOnNavMesh)
                    _nav.SetDestination(_enemy.PlayerPosition.position);
                return true;
            })
            .WithCompletion(() => _enemy.AttackSensor.IsTargetInRange
                            || !_enemy.DetectionSensor.IsTargetInRange);

        _goap.Actions.Add(_trackAction);
        _goap.CurrentGoal = _goap.Goals.First(g => g.Name == "KillPlayer");

        // Directly queue instead of planning — planner needs effects to be wired up first
        _goap.ActionPlan = new Queue<AgentAction>();
        _goap.ActionPlan.Enqueue(_trackAction);
        _goap.CurrentAction = null;
    }

    public override void Update()
    {
        // Let GOAP tick the current action
        if (_goap.CurrentAction == null || _goap.CurrentAction.IsDone())
        {
            if (_goap.ActionPlan.Count == 0) return;
            _goap.CurrentAction = _goap.ActionPlan.Dequeue();
        }

        if (!_goap.CurrentAction.Perform())
        {
            // Action failed — replan
            _goap.ActionPlan = GoapPlanner.Plan(_goap, _goap.CurrentGoal);
            _goap.CurrentAction = null;
        }
    }

    public override void OnExit()
    {
        _nav.ResetPath();
        _goap.Actions.Remove(_trackAction);
        _goap.CurrentAction = null;
        _goap.ActionPlan.Clear();
    }

}
