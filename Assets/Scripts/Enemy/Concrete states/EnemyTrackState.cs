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


    Vector2 FindPlayersPosition()
    {
        
        return Vector2.zero;
    }

}
