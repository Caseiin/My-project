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

        _goap.CurrentGoal = null;
        _goap.CurrentAction = null;
    }

    public override void OnExit()
    {
        _goap.CurrentAction?.Stop();
        _goap.CurrentAction = null;
        _goap.CurrentGoal = null;
    }

}
