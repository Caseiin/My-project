using UnityEngine;

[CreateAssetMenu(fileName = "ArcherActions", menuName = "Enemy/Actions/Archer")]
public class ArcherActions : BaseActionSetup
{
    public override void InitializeActions(GoapAgent agent, EnemyController enemy)
    {
        base.InitializeActions(agent, enemy);

        // Repostion produces belief Agent repositions
        agent.Actions.Add(new AgentAction.Builder("Reposition")
            .WithStrategy(new RepositionStrategy(agent.NavAgent, enemy.DetectionSensor.Radius, enemy.AttackSensor,enemy.DetectionSensor.Radius))
            .AddPreCondition(agent.Beliefs["PlayerDetected"])
            .AddEffect(agent.Beliefs["AgentRepositioned"])
            .Build());

        // After enemy repositions and still sees the player shoots the player
        agent.Actions.Add(new AgentAction.Builder("ShootPlayer")
            .WithStrategy(new SingleShotActionStrategy(attackStrategy,enemy))
            .AddPreCondition(agent.Beliefs["AgentRepositioned"])
            .AddPreCondition(agent.Beliefs["PlayerDetected"])
            .AddEffect(agent.Beliefs["PlayerDead"])
            .Build());
    }
}
