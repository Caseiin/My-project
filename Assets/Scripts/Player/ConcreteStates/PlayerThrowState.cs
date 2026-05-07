using System;
using UnityEngine;

public class PlayerThrowState : BaseState
{
    readonly PlayerController _player;
    readonly TutorialContextSO _aimContext;

    public PlayerThrowState(PlayerController player, TutorialContextSO aimContext) : base(player)
    {
        _player = player;
        _aimContext = aimContext;
    }

    public override void OnEnter()
    {
        _player.Input.OnAttackTriggered += HandleThrow;
        RegisterTutorial();
    }

    private void RegisterTutorial()
    {
        var policy = new PressurePhasePolicy(
            threshold: _aimContext.failure,
            inactiveTime: _aimContext.inactivityTimeout,
            showHint: ()=> Debug.Log(_aimContext.Hints),
            hindHint: ()=>Debug.Log("Hint hidden")
        );

        var aimTutorial = new TutorialData.Builder(_aimContext)
                            .WithPolicy(policy)
                            .WithCompletionCondition(
                                subscribe: cb => _player.Input.OnAimStarted += cb,
                                unsubscribe: cb => _player.Input.OnAimStarted -= cb
                            )
                            .WithInitialCondition(()=>Debug.Log($"Tutorial started: {_aimContext.TutorialName}"))
                            .WithEndCondition(()=>Debug.Log($"Tutorial completed: {_aimContext.TutorialName}"))
                            .Build();
        
        TutorialManager.Instance.AddTutorial(aimTutorial);
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
