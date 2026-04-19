using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseActioSetup", menuName = "Enemy/Actions")]
public abstract class BaseActionSetup : ScriptableObject
{
    public AttackStrategy attackStrategy;

    public virtual void InitializeActions(GoapAgent agent, EnemyController enemy){
        agent.Actions.Add(new AgentAction.Builder("Relax")
            .WithStrategy(new IdleStrategy(5))
            .AddEffect(agent.Beliefs["Nothing"])
            .Build());

        agent.Actions.Add(new AgentAction.Builder("Wander")
            .WithStrategy(new WanderStrategy(enemy.NavAgent, 10)) 
            .AddEffect(agent.Beliefs["AgentMoving"])
            .Build());

        agent.Actions.Add(new AgentAction.Builder("ChasePlayer")
            .WithStrategy(new ChaseStrategy(enemy.NavAgent,enemy.PlayerPosition,() => enemy.AttackSensor.IsTargetInRange)) 
            .AddPreCondition(agent.Beliefs["PlayerDetected"])
            .AddEffect(agent.Beliefs["PlayerInAttackRange"])
            .Build());

        agent.Actions.Add(new AgentAction.Builder("Flee")
            .WithStrategy(new FleeStrategy(enemy.NavAgent, enemy, 15))
            .AddPreCondition(agent.Beliefs["AgentNearDeath"])
            .AddEffect(agent.Beliefs["AgentFleeing"])
            .Build());
    }
}
