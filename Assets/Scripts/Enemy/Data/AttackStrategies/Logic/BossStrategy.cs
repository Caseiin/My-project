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

    public override void Initialize(){
        base.Initialize();
        countdownTimer.OnTimerStop += ResetCount;
    }

    public override void Attack(EnemyController enemy)
    {

        if(!CanAttack) return;

        enemy.PlayerPosition.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        _currentAttackCount++;

        if(!CanAttack)
            countdownTimer.Start();

    }

    public override void ResetCount() => _currentAttackCount = 0;

}