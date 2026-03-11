using System.Collections;
using UnityEngine;
using System;

public class EnemyDeathState : BaseState
{
    EnemyController _enemy;
    MonoBehaviour _runner;
    public static event Action OnEnemyDeath;
    public EnemyDeathState(EnemyController enemy) : base(enemy)
    {
        _enemy = enemy;
        _runner = _enemy as MonoBehaviour;
    }

    public override void OnEnter()
    {
        _runner.StartCoroutine(destroyEnemyRoutine());

    }

    IEnumerator destroyEnemyRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        // Fire event before destruction
        OnEnemyDeath?.Invoke();
        Debug.Log("Enemy died");

        // Then destroy the enemy
        UnityEngine.Object.Destroy(_enemy.gameObject);
    }


}
