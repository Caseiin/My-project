
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPopUp : MonoBehaviour
{
    Image _image;
    TextMeshProUGUI _text;

    void Awake()
    {
        _image = GetComponentInChildren<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();        
    }

    public void SetUp(string msg, Sprite icon)
    {
        _text.text = msg;
        _image.sprite = icon;
    }

    public void CleanUp()
    {
        _text.text = "";
        _image.sprite = null;
    }
}
