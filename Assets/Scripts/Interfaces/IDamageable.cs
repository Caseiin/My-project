using System;
using UnityEngine;

public interface IDamageable : IEffectable
{
    void TakeDamage(int dmg);
    void RestoreHealth(int health);
    event Action OnDeath;
}
