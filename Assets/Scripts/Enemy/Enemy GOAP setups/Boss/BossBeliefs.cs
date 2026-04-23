using UnityEngine;

[CreateAssetMenu(fileName = "ArcherBeliefs", menuName = "Enemy/Beliefs/Boss")]
public class BossBeliefs : BaseBeliefSetUps
{
    public override void InitialiseBelief(GoapAgent agent, EnemyController enemy)
    {
        base.InitialiseBelief(agent, enemy);
        // Add beliefs that maybe different
    }
}
