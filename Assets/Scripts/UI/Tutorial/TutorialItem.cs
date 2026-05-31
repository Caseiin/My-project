using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialItem : MonoBehaviour
{
    [SerializeField] GameObject _container;
    [SerializeField] TextMeshProUGUI _display;

    Action _onHide;

    public void Show(string hint, Action onHide){
        _onHide = onHide;
        _display.text = hint;
        _container.SetActive(true);
    }

    public void Hide(){
        _container.SetActive(false);
        _onHide?.Invoke();
        _onHide = null;
    }
}
