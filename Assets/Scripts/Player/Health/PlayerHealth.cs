using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Range(0,150)]
    [SerializeField] int _health;
    public int Health => _health;
    public event Action OnDeath;
    public event Action<int> OnHealthRestored;
    public event Action<int> OnHealthTaken;
    public event Action<int> OnHealthChanged;

    public void RestoreHealth(int health)
    {
        _health += health;
        OnHealthRestored?.Invoke(health);
        OnHealthChanged?.Invoke(_health);
    }

    public void TakeDamage(int dmg)
    {
        _health -= dmg;
        OnHealthTaken?.Invoke(dmg);
        OnHealthChanged?.Invoke(_health);

        if (_health <= 0) OnDeath?.Invoke();
    }
}
