using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FullScreenEffectController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ScriptableRendererFeature _fullScreenEffect;
    [SerializeField] Material _material;

    [Header("Colors")]
    [SerializeField] Color _damageColor = Color.red;
    [SerializeField] Color _healColor   = Color.green;

    [Header("Config")]
    [SerializeField] float _vignetteStart   = 1.76f;  // corners covered on hit
    [SerializeField] float _vignettePeak    = 5.2f;  // briefly tighten
    [SerializeField] float _defaultVignettePower = 3.15f;
    [SerializeField] float _fadeOutDuration = 1f;

    // Shader IDs
    static readonly int _ColorID = Shader.PropertyToID("_BaseColor");
    static readonly int _VignettePower = Shader.PropertyToID("_VignettePower");

    PlayerHealth _playerHealth;
    Coroutine _activeEffect;

    void Awake() => _playerHealth = Registry<PlayerController>.GetFirst().Health;

    void OnEnable()
    {
        _playerHealth.OnHealthTaken    += _value=> TriggerEffect(_damageColor);
        _playerHealth.OnHealthRestored += _value=> TriggerEffect(_healColor);
    }

    void OnDisable()
    {
        _playerHealth.OnHealthTaken    -= _value=> TriggerEffect(_damageColor);
        _playerHealth.OnHealthRestored -= _value=> TriggerEffect(_healColor);
    }

    void Start() => _fullScreenEffect.SetActive(false);

    void TriggerEffect(Color color)
    {
        if (_activeEffect != null)
            StopCoroutine(_activeEffect);

        _activeEffect = StartCoroutine(RunEffect(color));
    }

    IEnumerator RunEffect(Color effectcolor)
    {

        _material.SetColor(_ColorID, effectcolor);
        _material.SetFloat(_VignettePower, _defaultVignettePower);
        _fullScreenEffect.SetActive(true);

        float elapsed = 0f;
        while (elapsed < _fadeOutDuration){
            elapsed += Time.deltaTime;
            float timeLapsed = elapsed / _fadeOutDuration;
            _material.SetFloat(_VignettePower, Mathf.Lerp(_vignetteStart, _vignettePeak, timeLapsed));
            yield return null;
        }

        _fullScreenEffect.SetActive(false);
        _activeEffect = null;
    }
}
