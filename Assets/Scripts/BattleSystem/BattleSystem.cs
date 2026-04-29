using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] List<EnemySetUpSO> enemyBehaviourSetUps;
    EnemySpawn spawner = new();

    void Start()
    {
        StartBattle();
    }

    public void StartBattle()
    {
        Debug.Log("Start Battle");
        spawner.Spawn(enemy, enemyBehaviourSetUps[0]);
    }

    
}