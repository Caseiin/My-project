using UnityEngine;

public class PlayerIdleState : BaseState
{
    PlayerController _player;
    public PlayerIdleState(PlayerController player) : base(player)
    {
    }

}
