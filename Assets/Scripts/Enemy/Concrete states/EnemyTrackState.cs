using UnityEngine;

public class EnemyTrackState : BaseState
{
    EnemyController _enemy;
    Vector2 _destination;
    public EnemyTrackState(EnemyController enemy) : base(enemy)
    {
        _enemy = enemy;
    }

    public override void OnEnter() => Debug.Log("Enemy is tracking");
    public override void Update()
    {
        _destination = FindPlayersPosition();
        Debug.Log(_destination);
        Debug.Log("Track");

    }

    public override void FixedUpdate()
    {
        Track(_destination);
    }


    Vector2 FindPlayersPosition()
    {
        if (_enemy.PlayerPosition == null) return Vector2.zero;

        Vector3 enemyPos = _enemy.transform.position;
        Vector3 playerPos = _enemy.PlayerPosition.position;

        Vector2 direction = new Vector2(
            playerPos.x - enemyPos.x,
            playerPos.z - enemyPos.z
        ).normalized;

        return direction;
    }

    void Track(Vector2 direction)
    {
        _enemy.RB.linearVelocity = new Vector3(direction.x, 0, direction.y) * _enemy.MoveSpeed;
    }

}
