using System;
using UnityEngine;

[Serializable]
public class HealingEffect: Effect
{
    [SerializeField] int healingAmount;
    public override void Apply(IEffectable target)
    {
        var _target = target as IDamageable;
        Message = $"{this.GetType()}:gained +{healingAmount} hp";
        _target?.RestoreHealth(healingAmount);
    }
}
