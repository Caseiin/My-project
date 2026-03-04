using UnityEngine;

public class PlayerMotionState : BaseState
{  
    private PlayerController _player;

    public PlayerMotionState(PlayerController player) : base(player)
    {
        _player = player;
    }

    public override void OnEnter() => Debug.Log("Player is moving!");
    public override void OnExit() => _player.RB.linearVelocity = Vector2.zero;


    public override void FixedUpdate()
    {
        Move();

        if (_player.RB.linearVelocity.y < 0)
        {
            _player.RB.linearVelocity += Vector3.up * Physics.gravity.y * (_player.FallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void Move()
    {
        var move = new Vector3(_player.Input.MoveDirection.x, 0f, _player.Input.MoveDirection.y);
        _player.RB.linearVelocity = new Vector3(move.x * _player.MoveSpeed,_player.RB.linearVelocity.y,move.z * _player.MoveSpeed);
    }

    void Jump()
    {
        _player.RB.linearVelocity= new Vector3(_player.RB.linearVelocity.x, _player.JumpForce, _player.RB.linearVelocity.z);
    }
}
