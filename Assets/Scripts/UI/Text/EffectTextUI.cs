using UnityEngine;
using TMPro;
using System.Collections;

public class EffectTextUI : MonoBehaviour
{
    TextMeshPro _effectTxt;
    void Awake()
    {
        _effectTxt = GetComponent<TextMeshPro>();
    }

    void OnEnable()
    {
        Messenger.OnMessage += DisplayEffectMessage;
    }

    void OnDisable()
    {
        Messenger.OnMessage -= DisplayEffectMessage;
    }

    void DisplayEffectMessage(string msg)
    {
        StartCoroutine(DisplayEffect(msg));
    }

    IEnumerator DisplayEffect(string msg)
    {
        _effectTxt.text = msg;
        yield return new WaitForSeconds(1.5f);
        _effectTxt.text = "";
    }
}
