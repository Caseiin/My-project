using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Range(0,150)]
    [SerializeField] int _health;
    public int Health => _health;
    public event Action OnDeath;
    public event Action<int> OnHealthRestored;
    public event Action<int> OnHealthTaken;


    public void RestoreHealth(int health)
    {
        _health += health;
        OnHealthRestored?.Invoke(health);
    }

    public void TakeDamage(int dmg)
    {
        _health -= dmg;
        OnHealthTaken?.Invoke(dmg);

        // OnDeath?.Invoke();
    }
}
