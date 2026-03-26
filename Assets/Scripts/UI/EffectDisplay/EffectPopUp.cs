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
    
    Color _pendingColour;
    public void SetIcon(Sprite icon)
    {
        _icon.sprite = icon;
    }

    public void SetColour(Color color)
    {
        _background.color = color;
        _pendingColour = color;
    }

    public void DisplayImmediate(Action onComplete)
    {
        StartCoroutine(ImmediatedCoroutine( onComplete));
    }

    public void DisplayTimed(float duration, Action onComplete)
    {
        StartCoroutine(TimedCoroutine(duration,  onComplete));
    }

    IEnumerator TimedCoroutine(float duration, Action onComplete)
    {
        gameObject.SetActive(true);

        _background.enabled = false;

        _timer.enabled = true;
        _timer.color = _pendingColour;
        _timer.fillAmount = 0f;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _timer.fillAmount = elapsed / duration;
            yield return null;
        }

        _timer.fillAmount = 1f;

        onComplete?.Invoke();
        gameObject.SetActive(false);
    }

    IEnumerator ImmediatedCoroutine(Action onComplete)
    {
        // Hide timer, show background only
        _timer.enabled = false;
        _background.enabled = true;
        _background.color = _pendingColour;
        gameObject.SetActive(true);

        yield return new WaitForSeconds(immediateDuration);

        onComplete?.Invoke();
        gameObject.SetActive(false);
    }
}