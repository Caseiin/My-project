using UnityEngine;

[CreateAssetMenu(fileName = "MeleeActions", menuName = "Enemy/Actions/Melee")]
public class MeleeActions : BaseActionSetup
{
    public override void InitializeActions(GoapAgent agent, EnemyController enemy)
    {
        base.InitializeActions(agent, enemy);

        agent.Actions.Add(new AgentAction.Builder("ChasePlayer")
            .WithStrategy(new ChaseStrategy(enemy.NavAgent,enemy.PlayerPosition,() => enemy.AttackSensor.IsTargetInRange)) 
            .AddPreCondition(agent.Beliefs["PlayerDetected"])
            .AddEffect(agent.Beliefs["PlayerInAttackRange"])
            .Build());
        
        agent.Actions.Add(new AgentAction.Builder("MeleePlayer")
            .WithStrategy(new AttackActionStrategy(attackStrategy,enemy))
            .AddPreCondition(agent.Beliefs["PlayerInAttackRange"])
            .AddEffect(agent.Beliefs["PlayerDead"])
            .Build());

        agent.Actions.Add(new AgentAction.Builder("Flee")
            .WithStrategy(new FleeStrategy(enemy.NavAgent, enemy, 1.5f))
            .AddPreCondition(agent.Beliefs["AgentNearDeath"])
            .AddPreCondition(agent.Beliefs["PlayerDetected"])
            .AddEffect(agent.Beliefs["AgentFleeing"])
            .Build());
    }
}
