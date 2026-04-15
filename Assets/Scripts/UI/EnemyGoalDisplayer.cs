using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyGoalDisplayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    void OnEnable()
    {
        Messenger.OnMessage += DisplayDialogue;
    }

    void OnDisable()
    {
        Messenger.OnMessage -= DisplayDialogue;
    }

    void DisplayDialogue(string msg)
    {
        text.text = msg;
    }




}
