using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Health Info")]
    [SerializeField] FloatingNumberUI HealthPrefab;
    [Range(0,150)]
    [SerializeField] int _health;
    public int Health => _health;
    public int MaxHealth{get; private set;}
    
    public event Action OnDeath;
    public event Action<int> OnHealthRestored;
    public event Action<int> OnHealthTaken;

    void Start()
    {
        MaxHealth = _health;
    }

    public void RestoreHealth(int health)
    {
        _health += health;
        var healthUI = WorldSpaceUIManager.Instance.Spawn(HealthPrefab,transform);
        healthUI.SetHealth(health);
        OnHealthRestored?.Invoke(health);
    }

    public void TakeDamage(int dmg)
    {
        _health -= dmg;
        var dmgUI = WorldSpaceUIManager.Instance.Spawn(HealthPrefab,transform);
        dmgUI.SetDamage(dmg);
        OnHealthTaken?.Invoke(dmg);

        // OnDeath?.Invoke();
    }
}
