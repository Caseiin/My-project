using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FullScreenEffectController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ScriptableRendererFeature _fullScreenEffect;
    [SerializeField] Material _material;

    int _ColorID = Shader.PropertyToID("_BaseColor");
    int _VignettePower = Shader.PropertyToID("_VignettePower");
    const float default_vignette_power= 3.15f;


    void Start()
    {
        _fullScreenEffect.SetActive(false);
    }


    void DisplayEffect(float duration)
    {
        StartCoroutine(EffectDisplay(duration));
    }

    IEnumerator EffectDisplay(float duration)
    {
        _fullScreenEffect.SetActive(true);
        _material.SetFloat(_VignettePower, default_vignette_power);

        yield return new WaitForSeconds(duration);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // float lerpedVignette = Mathf.Lerp(default_vignette_power, 0f, (elapsedTime/ duration));

            // _material.SetFloat(_VignettePower, lerpedVignette);
            yield return null;
        }

        _fullScreenEffect.SetActive(false);
    }
}
