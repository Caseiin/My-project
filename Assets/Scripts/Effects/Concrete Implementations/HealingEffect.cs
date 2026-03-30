using System;
using UnityEngine;

[Serializable]
public class HealingEffect: GenEffect<IDamageable>
{
    [SerializeField] int healingAmount;

    protected override void ApplyEffect(IDamageable target)
    {
        Message = $"{this.GetType()}: That feels amazing!";
        target?.RestoreHealth(healingAmount);
    }
}
