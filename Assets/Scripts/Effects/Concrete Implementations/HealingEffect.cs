using System;
using UnityEngine;

[Serializable]
public class HealingEffect: Effect
{
    [SerializeField] int healingAmount;

    public override void Apply(IEffectable target)
    {
        var _target = target as IDamageable;
        Messenger.AddEffectMessage($"{this.GetType()}:Player gained +{healingAmount} hp");
        _target?.RestoreHealth(healingAmount);
    }
}
