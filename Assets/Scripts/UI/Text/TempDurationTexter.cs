using TMPro;
using UnityEngine;

public class TempDurationTexter : MonoBehaviour
{
    TextMeshPro _durationtext;

    void Awake()
    {
        _durationtext = GetComponent<TextMeshPro>();
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }

    void UpdateDuration(float duration)
    {
        _durationtext.text =$"Duration: {duration}";
    }
}
