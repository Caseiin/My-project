using UnityEngine;

[CreateAssetMenu(fileName = "ArcherBeliefs", menuName = "Enemy/Beliefs/Archer")]
public class ArcherBeliefs : BaseBeliefSetUps
{
    public override void InitialiseBelief(GoapAgent agent, EnemyController enemy)
    {
        base.InitialiseBelief(agent, enemy);

        var factory = new BeliefFactory(agent, agent.Beliefs);
        factory.AddBelief("AgentRepositioned", ()=> enemy.AttackSensor.Radius >= enemy.DetectionSensor.Radius *.9f);
        
    }
}
