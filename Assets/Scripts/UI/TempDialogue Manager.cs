using System;
using TMPro;
using UnityEngine;

public class TempDialogueManager : MonoBehaviour
{
    TextMeshProUGUI text;
    String message;
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        message = "Me:";
    }

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
        text.text = msg;
    }


}
