using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossStrategy", menuName = "Enemy/Attack/Boss")]
public class BossStrategy : AttackStrategy
{
    [SerializeField] int damage = 10;
    [SerializeField] int maxAttackCount = 3;
    [SerializeField] float cooldown = 2f;
    public override float CooldownDuration => cooldown;

    int _currentAttackCount = 0;
    bool CanAttack => _currentAttackCount < maxAttackCount;
    IPowerUp _power;
    EffectContext _effectContext;


    public override void Initialize(EnemyController enemy)
    {
        base.Initialize(enemy);
        countdownTimer.OnTimerStop += ResetCount;
        _effectContext = new EffectContext(enemy.Transform);
        _power = new EnemyPower(enemy.SetUp.Reward, _effectContext);
    }

    public override void Attack(EnemyController enemy)
    {
        if (!CanAttack) return;

        enemy.PlayerPosition.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        _currentAttackCount++;

        if (!CanAttack)
        {
            _power.UsePower();
            countdownTimer.Start();
            enemy.Health.IsInvulnerable = false;
        }
    }

    public override void ResetCount() => _currentAttackCount = 0;
}