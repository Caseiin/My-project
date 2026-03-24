using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class EffectPopUp : MonoBehaviour
{
    [SerializeField] Image _icon;

    public void SetIcon(Sprite icon)
    {
        _icon.sprite = icon;
    }

    public void DisplayImmediate()
    {
        // Show once, no timing
        gameObject.SetActive(true);
    }

    public void DisplayTimed(float duration, Action onComplete)
    {
        StartCoroutine(TimedCoroutine(duration, onComplete));
    }

    private IEnumerator TimedCoroutine(float duration, Action onComplete)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        onComplete?.Invoke();
    }
}
