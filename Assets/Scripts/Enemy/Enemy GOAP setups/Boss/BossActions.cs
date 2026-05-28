using UnityEngine;

[CreateAssetMenu(fileName = "BossActions", menuName = "Enemy/Actions/Boss")]
public class BossActions : BaseActionSetup
{
    public override void InitializeActions(GoapAgent agent, EnemyController enemy)
    {
        agent.Actions.Add(new AgentAction.Builder("Relax")
            .WithStrategy(new IdleStrategy(5))
            .AddEffect(agent.Beliefs["Nothing"])
            .Build());

        agent.Actions.Add(new AgentAction.Builder("AttackPlayer")
            .WithStrategy(new AttackActionStrategy(attackStrategy,enemy))
            .AddPreCondition(agent.Beliefs["PlayerInAttackRange"])
            .AddEffect(agent.Beliefs["PlayerDead"])
            .Build());

        agent.Actions.Add(new AgentAction.Builder("ChasePlayer")
            .WithStrategy(new ChaseStrategy(enemy.NavAgent,enemy.PlayerPosition,() => enemy.AttackSensor.IsTargetInRange)) 
            .AddPreCondition(agent.Beliefs["PlayerDetected"])
            .AddEffect(agent.Beliefs["PlayerInAttackRange"])
            .Build());
    }
}