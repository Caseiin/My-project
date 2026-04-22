using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileStrategy", menuName = "Enemy/Attack/ProjectileStrategy")]
public class ProjectileStrategy : AttackStrategy
{
    public GameObject projectile;
    public float forwardMag = 25f;
    
    public override void Attack(EnemyController enemy)
    {


        var spawnPosition = enemy.transform.position + enemy.transform.forward * 1.5f;
        var agent = enemy.NavAgent;
        agent.SetDestination(enemy.transform.position);

        // Face player before shooting
        if( enemy.PlayerPosition != null){
            enemy.transform.LookAt(enemy.PlayerPosition);
        }

        var rb = Instantiate(projectile,spawnPosition, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(enemy.transform.forward * forwardMag, ForceMode.Impulse);

        // trigger reposition cycle
        enemy.AttackSensor.Radius = 1f;
    }
}
