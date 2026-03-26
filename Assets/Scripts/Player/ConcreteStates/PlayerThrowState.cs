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

    public override void OnEnter()
    {
        Debug.Log("Throwing!");
        _trajector = _player._hand.GetComponentInChildren<TrajectorPredictor>(true);
        _player.Input.OnAttackTriggered += ThrowProjectile;

    } 
    void Aim(bool isAimming)
    {
        if (isAimming)
        {
            var throwPoint = _player._hand.transform;
            _trajector.Predict(throwPoint.position, throwPoint.forward * _player.Throw.ThrowForce);
        }
        else _trajector.StopPrediction();
    }

    public override void Update()
    {
        Aim(_player.Input.IsAimming);
    }

    void ThrowProjectile() => _player.Throw.Throw();

    public override void OnExit()
    {
        _player.Input.OnAttackTriggered -= ThrowProjectile;
    }


}
