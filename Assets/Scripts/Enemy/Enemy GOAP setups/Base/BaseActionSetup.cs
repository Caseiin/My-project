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

    }
}
