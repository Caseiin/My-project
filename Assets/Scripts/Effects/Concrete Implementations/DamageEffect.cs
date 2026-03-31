using System;
using UnityEngine;

[Serializable]
public class DamageEffect : GenEffect<IDamageable>
{
    [SerializeField] int dmgAmount;

    protected override void ApplyEffect(IDamageable target)
    {
        target?.TakeDamage(dmgAmount);
        Message = $"{this.GetType()}: Damn! I Took some Damage!";
        // Messenger.AddEffectMessage(Message);

    }
}
