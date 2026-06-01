using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] List<BattleData> battleData;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform spawnPoint;

    readonly Queue<BattleData> _battleQueue = new Queue<BattleData>();

    PlayerController _player;
    bool _waitingForNextSpawn;

    public static event Action<string> OnEnemySpawned;
    public static event Action OnBattleComplete;

    void Awake()
    {
        _player = Registry<PlayerController>.GetFirst();

        foreach (var data in battleData)
            _battleQueue.Enqueue(data);
    }

    void OnEnable()  => _player.Health.OnFullHealth += HandlePlayerFullHealth;
    void OnDisable() => _player.Health.OnFullHealth -= HandlePlayerFullHealth;

    void Start() => SpawnNext(); // Kick off the first enemy immediately

    void HandlePlayerFullHealth()
    {
        if (!_waitingForNextSpawn) return; // Guard: only act after an enemy death
        _waitingForNextSpawn = false;
        SpawnNext();
    }

    void SpawnNext()
    {
        if (_battleQueue.Count == 0)
        {
            OnBattleComplete?.Invoke();
            return;
        }

        var setup = _battleQueue.Dequeue();

        var enemy = Instantiate(enemyPrefab, spawnPoint)
            .GetComponent<EnemyController>();

        enemy.InitializeBehaviour(setup.SetupData, () => OnEnemyDied(enemy));

        OnEnemySpawned?.Invoke(setup.EnemyName);
    }

    void OnEnemyDied(EnemyController enemy)
    {
        Destroy(enemy.gameObject);
        
        if (_battleQueue.Count == 0)
        {
            OnBattleComplete?.Invoke();
            return;
        }

        _waitingForNextSpawn = true;   // Arm the flag — next full-health fires the spawn
        _player.Health.RestoreToFull(); // Trigger the restore; OnFullHealth will follow
    }
}