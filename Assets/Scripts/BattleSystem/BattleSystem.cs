using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] UnityEvent onBattleComplete;
    [SerializeField] List<BattleData> battleData;
    [SerializeField]PlayerController _player;

    readonly Queue<BattleData> _battleQueue = new Queue<BattleData>();

    bool _waitingForNextSpawn;

    public static event Action<string, string> OnEnemySpawned;

    void Start()
    {
        foreach (var data in battleData)
            _battleQueue.Enqueue(data);

        Debug.Log(_player.Health);
        if (_player == null)
        {
            Debug.Log("Player is null for battle system");
        }

        if(_player.Health == null){
            Debug.Log("Player Health is null for battle system");
            
        }
    }

    void OnEnable()
    {
        if (_player.Health == null) return;
        _player.Health.OnFullHealth += HandlePlayerFullHealth;
    } 
    void OnDisable()
    {
        if (_player.Health == null) return;
        _player.Health.OnFullHealth -= HandlePlayerFullHealth;
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
            _player.SetCameraLogic(new IdleCameraLogic(_player));
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
        if (_player == null || _player.Health == null) return;

        enemy.gameObject.SetActive(false);

        if (_battleQueue.Count == 0)
        {
            onBattleComplete?.Invoke();
            return;
        }

        _waitingForNextSpawn = true;
        _player.Health.RestoreToFull();
    }

    void ReleaseCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}