using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileStrategy", menuName = "Enemy/Attack/ProjectileStrategy")]
public class ProjectileStrategy : AttackStrategy
{
    public GameObject projectile;
    public float forwardMag = 32f;
    public float UpwardMag = 8f;
    
    public override void Attack(EnemyController enemy)
    {

        var agent = enemy.NavAgent;
        agent.SetDestination(enemy.transform.position);

        // enemy.transform.LookAt()

        var rb = Instantiate(projectile,enemy.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(enemy.transform.forward * forwardMag, ForceMode.Impulse);
        rb.AddForce(enemy.transform.up * UpwardMag, ForceMode.Impulse);

        // Decrease attack radius forcing goap to replan its action
        enemy.AttackSensor.Radius = 1f;
    }
}
