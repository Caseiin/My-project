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

    PlayerHealth _playerHealth = null;
    Coroutine _activeEffect;
    Action<int> _damageHandler;
    Action<int> _healHandler;


    void Awake()
    {
        _damageHandler = _ => TriggerEffect(_damageColor);
        _healHandler = _ => TriggerEffect(_healColor);
    }
    void OnEnable()
    {
        if (_playerHealth == null) return;

        _playerHealth.OnHealthTaken += _damageHandler;
        _playerHealth.OnHealthRestored += _healHandler;
        _playerHealth.OnDeath += ClearEffect;
    }

    void OnDisable()
    {
        if (_playerHealth == null) return;

        _playerHealth.OnHealthTaken -= _damageHandler;
        _playerHealth.OnHealthRestored -= _healHandler;
        _playerHealth.OnDeath -= ClearEffect;
    }

    void Start()
    {
        _playerHealth = Registry<PlayerController>.GetFirst().Health;
        if(_playerHealth == null){
            Debug.Log("player health is null");
            return;
        }

        _playerHealth.OnHealthTaken += _damageHandler;
        _playerHealth.OnHealthRestored += _healHandler;
        
        Debug.Log(_fullScreenEffect);
        Debug.Log(_material);
        ClearEffect();
    }

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
        ClearEffect();
    }

    void ClearEffect()
    {
        if (_activeEffect != null)
        {
            StopCoroutine(_activeEffect);
            _activeEffect = null;
        }

        _material.SetFloat(_VignettePower, 0f);
        _fullScreenEffect.SetActive(false);
    }
}
