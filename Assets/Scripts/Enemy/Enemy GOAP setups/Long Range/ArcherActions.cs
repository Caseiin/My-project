using UnityEngine;

[CreateAssetMenu(fileName = "ArcherActions", menuName = "Enemy/Actions/Archer")]
public class ArcherActions : BaseActionSetup
{
    public override void InitializeActions(GoapAgent agent, EnemyController enemy)
    {
        base.InitializeActions(agent, enemy);

        agent.Actions.Add(new AgentAction.Builder("Reposition")
            .WithStrategy(new RepositionStrategy(agent.NavAgent, enemy.DetectionSensor.Radius, enemy.AttackSensor,enemy.DetectionSensor.Radius))
            .AddPreCondition(agent.Beliefs["PlayerDetected"])
            .AddEffect(agent.Beliefs["PlayerInAttackRange"])
            .Build());

        agent.Actions.Add(new AgentAction.Builder("ShootPlayer")
            .WithStrategy(new AttackActionStrategy(attackStrategy,enemy))
            .AddPreCondition(agent.Beliefs["PlayerInAttackRange"])
            .AddEffect(agent.Beliefs["PlayerDead"])
            .Build());
    }
}
