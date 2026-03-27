using Unity.VisualScripting;
using UnityEngine;

public class PlayerThrowState : BaseState
{
    PlayerController _player;
    TrajectorPredictor _trajector;
    public PlayerThrowState(PlayerController player) : base(player)
    {
        _player = player;
    }

    public override void OnExit()
    {
        Debug.Log("Exit Throw");
    }


}
