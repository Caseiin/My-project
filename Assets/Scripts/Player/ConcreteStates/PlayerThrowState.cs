using System;
using UnityEngine;

public class PlayerThrowState : BaseState
{
    readonly PlayerController _player;
    public PlayerThrowState(PlayerController player) : base(player)
    {
        _player = player;
    }

    public override void OnEnter()
    {
        _player.Input.OnAttackTriggered += HandleThrow;
    }



    public override void Update()
    {
        Aim(_player.Input.IsAimming);
    }

    public override void OnExit()
    {
        _player.Trajectory.StopPrediction();
        _player.Input.OnAttackTriggered -= HandleThrow;

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

    public void HandleThrow()
    {
        _player.ThrowLogic.Throw();
    }

}
