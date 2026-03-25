using System;
using UnityEngine;

[Serializable]
public class HealingEffect: GenEffect<IDamageable>
{
    [SerializeField] int healingAmount;

    protected override void ApplyEffect(IDamageable target)
    {
        Message = $"{this.GetType()}:gained +{healingAmount} hp";
        target?.RestoreHealth(healingAmount);
    }
}
