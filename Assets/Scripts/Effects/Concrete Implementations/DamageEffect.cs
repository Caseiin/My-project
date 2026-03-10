using System;
using UnityEngine;

[Serializable]
public class DamageEffect : Effect
{
    [SerializeField] int dmgAmount;

    public override void Apply(IEffectable target)
    {
        var _target = target as IDamageable;
        Messenger.AddEffectMessage($"{this.GetType()}:lost -{dmgAmount} hp");
        _target?.TakeDamage(dmgAmount);
    }
}
