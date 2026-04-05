using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable,IPlayerEffectable
{
    [Range(0,150)]
    [SerializeField] int _health;
    [SerializeField] Slider HealthSlider;
    [SerializeField] TextMeshProUGUI HealthText;
    public int Health => _health;

    public int MaxHealth{get; private set;}

    int _totalHealth;
    public event Action OnDeath;
    public event Action<int> OnHealthRestored;
    public event Action<int> OnHealthTaken;
    public event Action<int> OnHealthChanged;

    void Awake()
    {
        _totalHealth = _health;
        MaxHealth = _totalHealth;
        HealthText.text = $"{_health}/{_totalHealth}";
    }

    public void RestoreHealth(int health)
    {
        _health += health;
        HealthSlider.value = (float)_health/_totalHealth;

        HealthText.text = $"{_health}/{_totalHealth}";

        OnHealthRestored?.Invoke(health);
        OnHealthChanged?.Invoke(_health);
    }

    public void TakeDamage(int dmg)
    {
        _health -= dmg;
        HealthSlider.value = (float)_health/_totalHealth;
        HealthText.text = $"{_health}/{_totalHealth}";

        OnHealthTaken?.Invoke(dmg);
        OnHealthChanged?.Invoke(_health);

        if (_health <= 0) OnDeath?.Invoke();
    }
}
