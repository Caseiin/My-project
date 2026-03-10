using System;
using UnityEngine;

[Serializable]
public class DamageEffect : Effect
{
    [SerializeField] int dmgAmount;
    public string Message;
    public override void Apply(IEffectable target)
    {
        var _target = target as IDamageable;
        Message = $"{this.GetType()}:lost -{dmgAmount} hp";
        _target?.TakeDamage(dmgAmount);
    }
}
