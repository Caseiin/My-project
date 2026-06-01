using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable,IPlayerEffectable
{
    [Header("Health")]
    [SerializeField] Slider HealthSlider;
    [SerializeField] TextMeshProUGUI HealthText;
    int _health;
    public int Health{get; private set;}
    public int MaxHealth{get; private set;}

    public event Action OnDeath;
    public event Action OnHealing;
    public event Action<int> OnHealthRestored;
    public event Action<int> OnHealthTaken;

    public void Initialize(int health){
        _health = health;
        Health = health;
        MaxHealth = health;        
        HealthText.text = $"{_health}/{MaxHealth}";
    }
    public void RestoreHealth(int health)
    {
        int remainderHealth =0;
        if(_health >= MaxHealth) return;

        if(_health + health >= MaxHealth){
            remainderHealth = MaxHealth - _health;
            _health += health;
        }
        else 
        _health += health;

        HealthSlider.value = (float)_health/MaxHealth;
        HealthText.text = $"{_health}/{MaxHealth}";

        OnHealthRestored?.Invoke(health);
        OnHealing?.Invoke();
    }

    public void TakeDamage(int dmg)
    {
        // ToDO: Death action must occur
        _health -= dmg;
        HealthSlider.value = (float)_health/MaxHealth;
        HealthText.text = $"{_health}/{MaxHealth}";

        OnHealthTaken?.Invoke(dmg);

        if (_health <= 0) 
            OnDeath?.Invoke();
    }
}
