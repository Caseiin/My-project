using UnityEngine;

public class PlayerIdleState : BaseState
{
    PlayerController _player;
    public PlayerIdleState(PlayerController player) : base(player)
    {
    }

    public override void OnEnter() => Debug.Log("Player is Idling!");
}
