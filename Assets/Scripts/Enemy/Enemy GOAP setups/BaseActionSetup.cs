using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseActioSetup", menuName = "Enemy/Actions")]
public class BaseActionSetup : ScriptableObject
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

            agent.Actions.Add(new AgentAction.Builder("AttackPlayer")
                .WithStrategy(new AttackActionStrategy(attackStrategy,enemy))
                .AddPreCondition(agent.Beliefs["PlayerInAttackRange"])
                .AddEffect(agent.Beliefs["PlayerDead"])
                .Build());
    }
}
