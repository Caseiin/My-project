using UnityEngine;
using TMPro;
using System.Collections;

public class EffectTextUI : MonoBehaviour
{
    TextMeshProUGUI _effectTxt;
    void Awake()
    {
        _effectTxt = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        Messenger.OnEffectMessage += DisplayEffectMessage;
    }

    void OnDisable()
    {
        Messenger.OnEffectMessage -= DisplayEffectMessage;
    }

    void DisplayEffectMessage(string msg)
    {
        Debug.Log(msg); 
        StartCoroutine(DisplayEffect(msg));
    }

    IEnumerator DisplayEffect(string msg)
    {
        _effectTxt.text = msg;
        yield return new WaitForSeconds(2f);
        _effectTxt.text = "";
    }
}
