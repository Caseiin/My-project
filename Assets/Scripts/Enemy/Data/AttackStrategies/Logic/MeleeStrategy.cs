using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeStrategy", menuName = "Enemy/Attack/Melee")]
public class MeleeStrategy : AttackStrategy
{
    [SerializeField] int damage = 10;
    [SerializeField] float cooldown = 1.5f;
    public override float CooldownDuration => cooldown;

    // TODO: Add visual effects to show attack happening
    public override void Attack(EnemyController enemy)
    {
        if (countdownTimer.IsRunning) return;

        enemy.PlayerPosition.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        countdownTimer.Start();
    }
}
