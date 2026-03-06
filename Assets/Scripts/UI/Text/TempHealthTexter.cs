using TMPro;
using UnityEngine;

public class TempHealthTexter : MonoBehaviour
{
    TextMeshPro _healthtext;

    void Awake()
    {
        _healthtext = GetComponent<TextMeshPro>();
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }

    void UpdateHealth(int health)
    {
        _healthtext.text =$"Health: {health}";
    }
}
