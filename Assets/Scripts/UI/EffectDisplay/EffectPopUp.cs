using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class EffectPopUp : MonoBehaviour
{
    [SerializeField] Image _icon;
    [SerializeField] Image _timer;

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
}
