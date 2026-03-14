using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileStrategy", menuName = "Enemy/Attack/ProjectileStrategy")]
public class ProjectileStrategy : AttackStrategy
{
    public GameObject projectile;
    public override void Attack(EnemyController enemy)
    {
        // 
    }
}
