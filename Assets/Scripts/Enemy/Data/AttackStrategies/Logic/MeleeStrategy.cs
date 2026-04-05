using UnityEngine;

[CreateAssetMenu(fileName = "MeleeStrategy", menuName = "Enemy/Attack/Melee")]
public class MeleeStrategy : AttackStrategy
{
    [SerializeField] int damage = 10;
    [SerializeField] float cooldown = 1.5f;
    
    float _lastAttackTime;

    void OnEnable()
    {
        // Resets every time the asset is loaded — including entering Play mode
        _lastAttackTime = -999f;
    }

    public override void Attack(EnemyController enemy)
    {
        if (Time.time - _lastAttackTime < cooldown) return;
        _lastAttackTime = Time.time;

        var hits = Physics.OverlapSphere(enemy.transform.position, enemy.AttackSensor.Radius);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<PlayerHealth>(out var health))
                health.TakeDamage(damage);
        }
    }
}
