using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TempBombEquipDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject textbox;
    void OnEnable()
    {
        Messenger.OnEquipMessage += DisplayEquippedBomb;
    }

    void OnDisable()
    {
        Messenger.OnEquipMessage -= DisplayEquippedBomb;
    }

    void DisplayEquippedBomb(string msg)
    {
        StartCoroutine(DisplayEquipped($"Equipped: {msg}"));
    }

    IEnumerator DisplayEquipped(string msg)
    {
        textbox.SetActive(true);
        text.text = msg;
        yield return new WaitForSeconds(3f);
        text.text ="";
        textbox.SetActive(false);
    }


}
