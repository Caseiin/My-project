using System.Collections;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    public SoundData Data { get; private set; }
    AudioSource _audioSource;
    Coroutine _playingCoroutine;
    bool _isReturned; // tracks if already returned to pool

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void InitializeSound(SoundData data)
    {
        Data = data;
        _isReturned = false; // reset every time emitter is initialized
        _audioSource.clip = data.Clip;
        _audioSource.outputAudioMixerGroup = data.mixerGroup;
        _audioSource.loop = data.Loop;
        _audioSource.playOnAwake = data.PlayonAwake;
        _audioSource.spatialBlend = data.SpatialBlend;
    }

    public void Play()
    {
        if (_playingCoroutine != null) StopCoroutine(_playingCoroutine);
        _audioSource.Play();
        _playingCoroutine = StartCoroutine(WaitForSoundEnd());
    }

    private IEnumerator WaitForSoundEnd()
    {
        yield return new WaitWhile(() => _audioSource.isPlaying);
        ReturnToPool();
    }

    public void Stop()
    {
        if (_playingCoroutine != null)
        {
            StopCoroutine(_playingCoroutine);
            _playingCoroutine = null;
        }

        _audioSource.Stop();
        ReturnToPool();
    }

    void ReturnToPool()
    {
        if (_isReturned) return; // guard against double release
        _isReturned = true;
        SoundManager.Instance.ReturnToPool(this);
    }

    public void RandomizePitch(float min = -0.02f, float max = 0.02f)
    {
        _audioSource.pitch += UnityEngine.Random.Range(min, max);
    }
}