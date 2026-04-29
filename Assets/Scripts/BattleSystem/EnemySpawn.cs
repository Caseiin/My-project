using UnityEngine;

public class EnemySpawn{
    public void Spawn(GameObject enemy, EnemySetUpSO setUp){
        Debug.Log("Enemy Spawned!");
       var _enemy =  GameObject.Instantiate(enemy).GetComponent<EnemyController>();
        _enemy.InitializeBehaviour(setUp);
    }
}