using System;
using UnityEngine;

public interface IDamageable : IEffectable
{
    void TakeDamage(int dmg);
    void RestoreHealth(int health);
    int Health{get;}
    event Action OnDeath;
    public event Action<int> OnHealthRestored;
    public event Action<int> OnHealthTaken;
}
