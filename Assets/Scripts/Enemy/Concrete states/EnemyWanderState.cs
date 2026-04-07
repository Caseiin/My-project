using UnityEngine;

public class EnemyWanderState : BaseState
{
    readonly WanderStrategy _wander;
    EnemyController _enemy;
    GoapAgent _goapAgent;
    public EnemyWanderState(EnemyController enemy,GoapAgent goapAgent) : base(enemy)
    {
        _enemy = enemy;
        _goapAgent = goapAgent;
        _wander = new WanderStrategy(goapAgent.NavAgent, 10f);
    }


}
