using System.Collections;
using UnityEngine;

public class EnemyDeathState : BaseState
{
    EnemyController _enemy;
    MonoBehaviour _runner;
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
        yield return new WaitForSeconds(1.5f);
        Object.Destroy(_enemy.gameObject);
    }


}
