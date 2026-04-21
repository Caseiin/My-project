using UnityEngine;

public abstract class BaseGoalsSetup : ScriptableObject
{
    public virtual void InitialiseGoals(GoapAgent agent){
        agent.Goals.Add(new AgentGoal.Builder("ChillOut")
            .WithPriority(1)
            .WithDesiredEffect(agent.Beliefs["Nothing"])
            .Build());

        agent.Goals.Add(new AgentGoal.Builder("Wander")
            .WithPriority(2)
            .WithDesiredEffect(agent.Beliefs["AgentMoving"])
            .Build());

        agent.Goals.Add(new AgentGoal.Builder("KillPlayer")
            .WithPriority(3)
            .WithDesiredEffect(agent.Beliefs["PlayerDead"])
            .Build());
    }
}
