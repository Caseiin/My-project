using UnityEngine;

public class PlayerThrowState : BaseState
{
    PlayerController _player;
    TrajectorPredictor _trajector;
    public PlayerThrowState(PlayerController player) : base(player)
    {
        _player = player;
    }

    public override void Update()
    {
        // Aim(Input.IsAimming);
    }

    public override void OnExit()
    {
        
    }

    public void Aim(bool IsAimming)
    {
        if (IsAimming)
        {
            var velocity = _player.ThrowLogic.CalculateThrowVelocity();
            _player.Trajectory.Predict(_player.Hand.transform.position,velocity);
        }
        else
        {
            _player.Trajectory.StopPrediction();
        }
    }

}
