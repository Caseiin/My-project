using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] List<EnemySetUpSO> enemyBehaviourSetUps;
    [SerializeField] int level = 1;
    [SerializeField] int baseEnemyCount = 3;

    EnemySpawn spawner = new();
    int _activeEnemies = 0;

    void Start()
    {
        StartBattle();
    }

    public void StartBattle()
    {
        Debug.Log($"Start Battle - Level {level}");
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        int enemyCount = baseEnemyCount + (level - 1);
        _activeEnemies = enemyCount;

        Debug.Log($"Spawning wave: {enemyCount} enemies");

        for (int i = 0; i < enemyCount; i++)
        {
            EnemySetUpSO behaviour = PickBehaviour();
            spawner.Spawn(enemy, behaviour, OnEnemyDied);
        }

        yield break;
    }

    void OnEnemyDied()
    {
        _activeEnemies--;
        Debug.Log($"Enemy died. Remaining: {_activeEnemies}");

        if (_activeEnemies <= 0)
        {
            OnWaveCleared();
        }
    }

    void OnWaveCleared()
    {
        level++;
        Debug.Log($"Wave cleared! Starting level {level}");
        StartCoroutine(SpawnWave());
    }

    EnemySetUpSO PickBehaviour()
    {
        int unlocked = Mathf.Min(level, enemyBehaviourSetUps.Count);
        return enemyBehaviourSetUps[Random.Range(0, unlocked)];
    }
}