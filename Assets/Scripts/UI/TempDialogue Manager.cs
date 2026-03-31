using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TempDialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    void OnEnable()
    {
        Messenger.OnEffectMessage += DisplayEffectDialogue;
    }

    void OnDisable()
    {
        Messenger.OnEffectMessage -= DisplayEffectDialogue;
    }

    void DisplayEffectDialogue(string msg)
    {
        Debug.Log($"Displayed the effect:{msg}");
        StartCoroutine(DisplayEffect(msg));
    }

    IEnumerator DisplayEffect(string msg)
    {
        text.text = msg;
        yield return new WaitForSeconds(3f);
        text.text ="";
    }


}
