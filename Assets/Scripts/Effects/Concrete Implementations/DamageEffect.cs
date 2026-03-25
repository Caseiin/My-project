using System;
using UnityEngine;

[Serializable]
public class DamageEffect : GenEffect<IDamageable>
{
    [SerializeField] int dmgAmount;

    protected override void ApplyEffect(IDamageable target)
    {
        target?.TakeDamage(dmgAmount);
        Message = $"{this.GetType()}:lost -{dmgAmount} hp";
    }
}
