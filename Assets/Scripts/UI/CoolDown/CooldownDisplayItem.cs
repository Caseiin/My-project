using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownDisplayItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _label;
    [SerializeField] Slider _slider;
    [SerializeField] Image _sliderFill;

    public event Action<CooldownDisplayItem> OnComplete;

    float _duration;
    float _elapsed;
    bool _running;
    Action<CooldownDisplayItem> _onComplete;
    public void Activate(string label, float duration, Action<CooldownDisplayItem> onComplete,Color? color)
    {
        _label.text   = label;
        _duration     = duration;
        _elapsed      = 0f;
        _slider.value = 0f;
        _onComplete   = onComplete;
        _running      = true;

        Color resolvedColor   = color ?? Color.white;
        _label.color          = resolvedColor;
        _sliderFill.color     = resolvedColor;
    }

    void Update()
    {
        if (!_running) return;

        _elapsed += Time.deltaTime;
        _slider.value = _elapsed / _duration;

        if (_elapsed >= _duration)
            Complete();
    }

    void Complete()
    {
        _running      = false;
        _slider.value = 1f;
        _onComplete?.Invoke(this);
        _onComplete   = null; // clear so stale refs don't linger in the pool
    }

    // If the same ability re-triggers while still displayed, restart it
    public void Restart(float duration)
    {
        _duration = duration;
        _elapsed = 0f;
        _running = true;
    }

    public string Label => _label.text;
}