using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEmitter : MonoBehaviour
{
    public SoundData Data {get; private set;}
    AudioSource _audioSource;
    Coroutine _playingCoroutine;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void InitializeSound(SoundData data)
    {
        Data = data;
        _audioSource.clip = data.Clip;
        _audioSource.outputAudioMixerGroup = data.mixerGroup;
        _audioSource.loop = data.Loop;
        _audioSource.playOnAwake = data.PlayonAwake;
    }
    public void Play()
    {
        if (_playingCoroutine != null){
            StopCoroutine(_playingCoroutine);
        }

        _audioSource.Play();
        StartCoroutine(WaitForSoundEnd());
    }

    private IEnumerator WaitForSoundEnd()
    {
        yield return new WaitWhile(()=> _audioSource.isPlaying);
        SoundManager.Instance.ReturnToPool(this);
    }

    public void Stop()
    {
        if (_playingCoroutine != null){
            StopCoroutine(_playingCoroutine);
            _playingCoroutine = null;
        }

        _audioSource.Stop();
        SoundManager.Instance.ReturnToPool(this);
    }

    public void RandomizePitch(float min = -0.05f, float max = 0.05f)
    {
        _audioSource.pitch += UnityEngine.Random.Range(min,max);
    }
}

