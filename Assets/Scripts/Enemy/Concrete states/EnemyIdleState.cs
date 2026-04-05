using UnityEngine;

public class EnemyIdleState : BaseState
{
    EnemyController _enemy;
    GoapAgent _goap;

    public EnemyIdleState(EnemyController enemy, GoapAgent goap) : base(enemy)
    {
        _enemy = enemy;
        _goap = goap;
    }

    public override void OnEnter()
    {
        Debug.Log("Enemy is idling");

        // Stop the agent when entering idle
        _goap.NavAgent.ResetPath();

        // GOAP: register a wait/patrol action only valid in idle
        _idleAction = new AgentAction("WaitInIdle")
            .WithCost(1f)
            .WithPerformance(() => true)                           // do nothing, just stand
            .WithCompletion(() => _enemy.DetectionSensor.IsTargetInRange); // done when player detected

        _goap.Actions.Add(_idleAction);
    }

    public override void OnExit()
    {
        _goap.Actions.Remove(_idleAction);
    }

    AgentAction _idleAction;
}
