using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Range(0,150)]
    [SerializeField] int _health;
    public int Health => _health;
    public event Action OnDeath;

    public void RestoreHealth(int health)
    {
        _health += health;
    }

    public void TakeDamage(int dmg)
    {
        _health -= dmg;
        OnDeath?.Invoke();
    }
}
