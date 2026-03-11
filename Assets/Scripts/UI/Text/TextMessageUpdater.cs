using TMPro;
using UnityEngine;

public class TextMessageUpdater : MonoBehaviour
{
    TextMeshProUGUI _messageTxt;
    void Awake()
    {
        _messageTxt = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        Messenger.OnMessage += DisplayMessage;
    }

    void OnDisable()
    {
        Messenger.OnMessage -= DisplayMessage;
    }

    void DisplayMessage(string msg)
    {
        _messageTxt.text = msg;
    }


}
