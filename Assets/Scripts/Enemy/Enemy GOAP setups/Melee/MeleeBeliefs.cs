using UnityEngine;

[CreateAssetMenu(fileName = "MeleeBeliefs", menuName = "Enemy/Beliefs/Melee")]
public class MeleeBeliefs : BaseBeliefSetUps
{
    public override void InitialiseBelief(GoapAgent agent, EnemyController enemy)
    {
        base.InitialiseBelief(agent, enemy);
        BeliefFactory factory = new BeliefFactory(agent, agent.Beliefs);
        factory.AddBelief("AgentFleeing", ()=> false);
    }
}
