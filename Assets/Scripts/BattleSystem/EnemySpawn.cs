using UnityEngine;

public class EnemySpawn {
    public void Spawn(GameObject enemy, EnemySetUpSO setUp, System.Action onDeath) {
        var _enemy = GameObject.Instantiate(enemy).GetComponent<EnemyController>();
        _enemy.InitializeBehaviour(setUp, onDeath);
    }
}