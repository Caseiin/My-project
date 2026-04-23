using UnityEngine;

[CreateAssetMenu(fileName = "ArcherActions", menuName = "Enemy/Actions/Boss")]
public class BossActions : BaseActionSetup
{
    public override void InitializeActions(GoapAgent agent, EnemyController enemy)
    {
        base.InitializeActions(agent, enemy);

        // Add boss actions
    }
}