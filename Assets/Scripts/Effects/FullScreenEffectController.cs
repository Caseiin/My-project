using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FullScreenEffectController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ScriptableRendererFeature _fullScreenEffect;
    [SerializeField] Material _material;

    [Header("Config")]
    [SerializeField] float _vignetteStart   = 1.76f;  // corners covered on hit
    [SerializeField] float _vignettePeak    = 5.2f;  // briefly tighten
    [SerializeField] float _defaultVignettePower = 3.15f;
    [SerializeField] float _fadeOutDuration = 1f;

    // Shader IDs
    static readonly int _ColorID = Shader.PropertyToID("_BaseColor");
    static readonly int _VignettePower = Shader.PropertyToID("_VignettePower");

    Coroutine _activeEffect;

    void Start()=> _fullScreenEffect.SetActive(false);

    void OnEnable() => AbilityProjectile.OnPlayerEffectLanded += DisplayEffect;
    void OnDisable() => AbilityProjectile.OnPlayerEffectLanded -= DisplayEffect;


    public void DisplayEffect(Color effectcolor, float holdDuration){
        if(_activeEffect != null)
            StopCoroutine(_activeEffect);

        _activeEffect = StartCoroutine(RunEffect(effectcolor, holdDuration));
    }

    IEnumerator RunEffect(Color effectcolor, float holdDuration)
    {
        //TODO: Fix the motionless duration vignette its not working
        Debug.Log($"[Vignette] DisplayEffect called | Duration: {holdDuration}");

        _material.SetColor(_ColorID, effectcolor);
        _material.SetFloat(_VignettePower, _defaultVignettePower);
        _fullScreenEffect.SetActive(true);

        yield return new WaitForSeconds(holdDuration);

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
