using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] UnityEvent onBattleComplete;
    [SerializeField] List<BattleData> battleData;
    PlayerHealth _playerhealth;

    readonly Queue<BattleData> _battleQueue = new Queue<BattleData>();

    bool _waitingForNextSpawn;

    public static event Action<string, string> OnEnemySpawned;

    void Start()
    {
        _playerhealth = Registry<PlayerController>.GetFirst().GetComponent<PlayerHealth>();
        foreach (var data in battleData)
            _battleQueue.Enqueue(data);



        if(_playerhealth == null){
            Debug.Log("Player Health is null for battle system");
            return;
        }

        Debug.Log(_playerhealth);
        _playerhealth.OnFullHealth += HandlePlayerFullHealth;
    }

    void OnEnable()
    {
        if (_playerhealth== null) return;
        _playerhealth.OnFullHealth += HandlePlayerFullHealth;
    } 
    void OnDisable()
    {
        if (_playerhealth == null) return;
       _playerhealth.OnFullHealth -= HandlePlayerFullHealth;
    } 

    // void Start() => SpawnNext();

    void HandlePlayerFullHealth()
    {
        if (!_waitingForNextSpawn) return;
        _waitingForNextSpawn = false;
        SpawnNext();
    }

    public void SpawnNext()
    {
        if (_battleQueue.Count == 0)
        {
            ReleaseCursor();
            onBattleComplete?.Invoke();
            return;
        }

        var next = _battleQueue.Dequeue();
        next.Enemy.gameObject.SetActive(true); // Awake/Start run here, fully clean
        OnEnemySpawned?.Invoke(next.EnemyName, next.Weakness);

        next.Enemy.Health.OnDeath += () => OnEnemyDied(next.Enemy);
    }

    void OnEnemyDied(EnemyController enemy)
    {
        if (enemy == null) return;
        if (_playerhealth == null) return;

        enemy.gameObject.SetActive(false);

        if (_battleQueue.Count == 0)
        {
            onBattleComplete?.Invoke();
            return;
        }

        _waitingForNextSpawn = true;
        _playerhealth.RestoreToFull();
    }

    void ReleaseCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}