using UnityEngine;

[CreateAssetMenu(fileName = "BossGoals", menuName = "Enemy/Goals/Boss")]
public class BossGoals : BaseGoalsSetup
{
    public override void InitialiseGoals(GoapAgent agent)
    {
        agent.Goals.Add(new AgentGoal.Builder("ChillOut")
            .WithPriority(1)
            .WithDesiredEffect(agent.Beliefs["Nothing"])
            .Build());

        agent.Goals.Add(new AgentGoal.Builder("KillPlayer")
            .WithPriority(3)
            .WithDesiredEffect(agent.Beliefs["PlayerDead"])
            .Build());
    }
}
