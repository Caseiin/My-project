using UnityEngine;

public class PlayerMotionState : BaseState
{
    
    private PlayerController player;

    public PlayerMotionState(PlayerController player) : base(player)
    {
        this.player = player;
    }

    public override void OnEnter() => Debug.Log("Player is moving!");


    public override void FixedUpdate()
    {
        Move();

        if (player.RB.linearVelocity.y < 0)
        {
            player.RB.linearVelocity += Vector3.up * Physics.gravity.y * (player.FallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void Move()
    {
        var move = new Vector3(player.Input.MoveDirection.x, 0f, player.Input.MoveDirection.y);
        player.RB.linearVelocity = new Vector3(move.x * player.MoveSpeed,player.RB.linearVelocity.y,move.z * player.MoveSpeed);
    }

    void Jump()
    {
        player.RB.linearVelocity= new Vector3(player.RB.linearVelocity.x, player.JumpForce, player.RB.linearVelocity.z);
    }
}
