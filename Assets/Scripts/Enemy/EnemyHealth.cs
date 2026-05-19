using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Health Info")]
    [SerializeField] FloatingNumberUI HealthPrefab;
    int _health;
    public int Health => _health;
    public int MaxHealth{get; private set;}
    
    public event Action OnDeath;
    public event Action<int> OnHealthRestored;
    public event Action<int> OnHealthTaken;

    public void InitializeHealth(EnemySetUpSO setUp, Action onDeath = null){
        _health = setUp.Health;
        MaxHealth = _health;
        if (onDeath != null) OnDeath += onDeath;
    }

    public void RestoreHealth(int health)
    {
        var remainderHealth = 0;
        if(_health >= MaxHealth) return;

        if(_health + health >= MaxHealth){
            remainderHealth = MaxHealth - _health;
            _health += health;
        }
        else 
        _health += health;
        
        var healthUI = WorldSpaceUIManager.Instance.Spawn(HealthPrefab,transform);
        healthUI.SetHealth(health);
        OnHealthRestored?.Invoke(health);
    }

    public void TakeDamage(int dmg)
    {
        _health -= dmg; // subtract first

        var dmgUI = WorldSpaceUIManager.Instance.Spawn(HealthPrefab, transform);
        dmgUI.SetDamage(dmg);
        OnHealthTaken?.Invoke(dmg);

        if (_health <= 0) // then check
        {
            WorldSpaceUIManager.Instance.ReleaseToPools(HealthPrefab, dmgUI);
            Die();
        }

    }

    void Die(){
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
