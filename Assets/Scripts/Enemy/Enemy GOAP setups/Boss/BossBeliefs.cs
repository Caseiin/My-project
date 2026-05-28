using UnityEngine;

[CreateAssetMenu(fileName = "BossBeliefs", menuName = "Enemy/Beliefs/Boss")]
public class BossBeliefs : BaseBeliefSetUps
{
    public override void InitialiseBelief(GoapAgent agent, EnemyController enemy)
    {
        base.InitialiseBelief(agent, enemy);
        
        BeliefFactory factory = new BeliefFactory(agent, agent.Beliefs);
        factory.AddBelief("AgentIsInVulnerable", ()=> enemy.Health.IsInVulnerable);
    }
}
