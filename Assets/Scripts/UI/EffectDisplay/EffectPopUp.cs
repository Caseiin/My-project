using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class EffectPopUp : MonoBehaviour
{
    [SerializeField] Image _icon;
    [SerializeField] Image _timer;
    [SerializeField] Image _background;
    [SerializeField] float immediateDuration = 0.3f; 

    public void SetIcon(Sprite icon)
    {
        _icon.sprite = icon;
    }

    public void DisplayImmediate(Action onComplete)
    {
        StartCoroutine(ImmediatedCoroutine(onComplete));
    }

    public void DisplayTimed(float duration, Action onComplete)
    {
        StartCoroutine(TimedCoroutine(duration, onComplete));
    }

    IEnumerator TimedCoroutine(float duration, Action onComplete)
    {
        gameObject.SetActive(true);
        _timer.enabled = true;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _timer.fillAmount = Mathf.Lerp(0f, 1f, elapsed / duration);
            yield return null;
        }

        _timer.fillAmount = 1f; 
        onComplete?.Invoke();
        _timer.enabled = false;
    }

    IEnumerator ImmediatedCoroutine(Action onComplete)
    {
        gameObject.SetActive(true);
        _background.enabled = true;
        yield return new WaitForSeconds(immediateDuration);
        onComplete?.Invoke();
        _background.enabled = false;
    }
    
}
