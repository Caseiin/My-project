using UnityEngine;
using TMPro;
using System.Collections;

public class HealthTextDisplayer : MonoBehaviour
{
    TextMeshProUGUI _healthTxt;
    PlayerHealth _health;
    void Awake()
    {
        _healthTxt = GetComponent<TextMeshProUGUI>();
        _health = FindFirstObjectByType<PlayerHealth>();
    }

    void OnEnable()
    {
        _health.OnHealthChanged += DisplayHealthMessage;
    }

    void OnDisable()
    {
        _health.OnHealthChanged -= DisplayHealthMessage;
    }

    void DisplayHealthMessage(int health)
    {
        _healthTxt.text = $"Health: {health}";
    }


}
