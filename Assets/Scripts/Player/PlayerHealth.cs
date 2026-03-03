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
        Debug.Log($"health is restored... new health {_health}");
    }

    public void TakeDamage(int dmg)
    {
        _health -= dmg;
        Debug.Log($"health is taken... new health {_health}");
    }
}
