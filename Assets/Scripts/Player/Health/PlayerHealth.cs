using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable, IPlayerEffectable
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI healthText;

    public int Health    { get; private set; }
    public int MaxHealth { get; private set; }

    public event Action       OnDeath;
    public event Action       OnHealing;
    public event Action       OnFullHealth;
    public event Action<int>  OnHealthRestored;
    public event Action<int>  OnHealthTaken;

    public void Initialize(int health)
    {
        Health = MaxHealth = health;
        RefreshUI();
    }

    public void RestoreToFull()
    {
        if (Health >= MaxHealth) 
        {
            OnFullHealth?.Invoke(); // Already full — still fire so the queue advances
            return;
        }

        Health = MaxHealth;
        RefreshUI();
        OnHealing?.Invoke();
        OnHealthRestored?.Invoke(MaxHealth); // Restored to full by definition
        OnFullHealth?.Invoke();
    }

    public void RestoreHealth(int amount)
    {
        if (Health >= MaxHealth) return;

        int actual = Mathf.Min(amount, MaxHealth - Health); // Clamp to remainder
        Health += actual;

        RefreshUI();
        OnHealing?.Invoke();
        OnHealthRestored?.Invoke(actual);

        if (Health == MaxHealth)
            OnFullHealth?.Invoke();
    }

    public void TakeDamage(int dmg)
    {
        Health = Mathf.Max(Health - dmg, 0); // Clamp — never go below zero
        RefreshUI();
        OnHealthTaken?.Invoke(dmg);

        if (Health <= 0)
            OnDeath?.Invoke();
    }

    void RefreshUI()
    {
        healthSlider.value = (float)Health / MaxHealth;
        healthText.text    = $"{Health}/{MaxHealth}";
    }
}