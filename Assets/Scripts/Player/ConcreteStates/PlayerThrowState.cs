using UnityEngine;

public class PlayerThrowState : BaseState
{
    PlayerController _player;
    // TutorialData _aimTutorial;
    // TutorialData _throwTutorial;

    public PlayerThrowState(PlayerController player) : base(player)
    {
        _player = player;

        // _aimTutorial = new TutorialData.Builder("Aim")
        //                     .WithCompletionCondition(
        //                         subscribe: callback => _player.Input.OnAimStarted += callback,
        //                         unsubscribe: callback => _player.Input.OnAimStarted -= callback
        //                     )
        //                     .WithInitialCondition(()=>Debug.Log("Press Right Mouse Button to Aim"))
        //                     .WithEndCondition(()=>Debug.Log("Aim tutorial Complete"))
        //                     .Build();

        // TutorialManager.Instance.AddTutorial(_aimTutorial);
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
        Debug.Log("Player is throwing");
    }

}
