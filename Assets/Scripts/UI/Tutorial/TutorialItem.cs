using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialItem : MonoBehaviour
{
    [SerializeField] GameObject _container;
    [SerializeField] TextMeshProUGUI _display;

    Action _onHide;

    public void Show(string hint, float duration, Action onHide){
        _onHide = onHide;
        _display.text = hint;
        _container.SetActive(true);

        if(duration> 0f)
            StartCoroutine(AutoHide(duration));
    }

    IEnumerator AutoHide(float duration)
    {
        yield return new WaitForSeconds(duration);
        Hide();
    }

    public void Hide(){
        StopAllCoroutines();
        _container.SetActive(false);
        _onHide?.Invoke();
        _onHide = null;
    }
}
