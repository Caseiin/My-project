using UnityEngine;

[CreateAssetMenu(fileName = "MeleeStrategy", menuName = "Enemy/Attack/Melee")]
public class MeleeStrategy : AttackStrategy
{
    public float damage;
    public float range;

    public override void Attack(EnemyController enemy)
    {
        // 
    }
}
