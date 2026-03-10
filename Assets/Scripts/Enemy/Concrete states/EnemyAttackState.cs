using UnityEngine;

public class EnemyAttackState : BaseState
{
    EnemyController _enemy;
    PlayerHealth _playerHealth;

    public EnemyAttackState(EnemyController enemy) : base(enemy)
    {
        _enemy = enemy;
    }

    public override void OnEnter()
    {
        Debug.Log("Enemy is attacking");
        _playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
        _playerHealth.TakeDamage(30);

        Debug.Log("Enemy hit player for 30 dmg");
    }
}
