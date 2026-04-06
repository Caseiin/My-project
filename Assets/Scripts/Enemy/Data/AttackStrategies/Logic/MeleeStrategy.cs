using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeStrategy", menuName = "Enemy/Attack/Melee")]
public class MeleeStrategy : AttackStrategy
{
    [SerializeField] int damage = 10;
    [SerializeField] float cooldown = 1.5f;
    
    Dictionary<EnemyController, float> _lastAttackTimes;
    void OnEnable() => _lastAttackTimes = new Dictionary<EnemyController, float>();
    public override void Attack(EnemyController enemy)
    {
        _lastAttackTimes.TryGetValue(enemy, out float lastTime);
        if (Time.time - lastTime < cooldown) return;
        
        _lastAttackTimes[enemy] = Time.time;
        enemy.PlayerPosition.GetComponent<PlayerHealth>()?.TakeDamage(damage);
    }
}
