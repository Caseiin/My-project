using UnityEngine;

[CreateAssetMenu(fileName = "MeleeActions", menuName = "Enemy/Actions/Melee")]
public class MeleeActions : BaseActionSetup
{
    public override void InitializeActions(GoapAgent agent, EnemyController enemy)
    {
        base.InitializeActions(agent, enemy);
        
        agent.Actions.Add(new AgentAction.Builder("AttackPlayer")
            .WithStrategy(new AttackActionStrategy(attackStrategy,enemy))
            .AddPreCondition(agent.Beliefs["PlayerInAttackRange"])
            .AddEffect(agent.Beliefs["PlayerDead"])
            .Build());
    }
}
