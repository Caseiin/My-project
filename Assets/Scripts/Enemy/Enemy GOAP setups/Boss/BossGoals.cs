using UnityEngine;

[CreateAssetMenu(fileName = "ArcherGoals", menuName = "Enemy/Goals/Boss")]
public class BossGoals : BaseGoalsSetup
{
    public override void InitialiseGoals(GoapAgent agent)
    {
        base.InitialiseGoals(agent);
        // Add boss goals if different from other enemies
    }
}
