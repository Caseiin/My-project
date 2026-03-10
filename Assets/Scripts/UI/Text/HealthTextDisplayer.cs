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
        
    }

    void OnDisable()
    {
        
    }

    void DisplayEffectMessage(int health)
    {
        StartCoroutine(DisplayHealth(health));
    }

    IEnumerator DisplayHealth(int health)
    {
        _healthTxt.text = $"{health}";
        yield return new WaitForSeconds(2f);
        _healthTxt.text = "";
    }
}
