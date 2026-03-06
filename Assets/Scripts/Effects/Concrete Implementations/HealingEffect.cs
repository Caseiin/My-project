using System;
using UnityEngine;

[Serializable]
public class HealingEffect: Effect
{
    [SerializeField] int healingAmount;
    Messenger _messenger;
    public override void Apply(IEffectable target)
    {
        var _target = target as IDamageable;
        _messenger.AddEffectMessage(this,$"Player gained +{healingAmount} hp");
        _target?.RestoreHealth(healingAmount);
    }
}
