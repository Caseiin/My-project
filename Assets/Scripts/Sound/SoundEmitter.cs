using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEmitter : MonoBehaviour
{
    AudioSource _audioSource;
    Coroutine _playingCoroutine;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void InitializeSound(SoundData data)
    {
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


}
