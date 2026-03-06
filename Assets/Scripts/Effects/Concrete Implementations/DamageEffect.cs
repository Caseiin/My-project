using System;
using UnityEngine;

[Serializable]
public class DamageEffect : Effect
{
    [SerializeField] int dmgAmount;
    Messenger _messenger;

    public override void Apply(IEffectable target)
    {
        var _target = target as IDamageable;
        _messenger = new();
        _messenger.AddEffectMessage(this,$"Player lost -{dmgAmount} hp");
        _target?.TakeDamage(dmgAmount);
    }
}
