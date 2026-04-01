using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEmitter : MonoBehaviour
{
    AudioSource _audioSource;
    Coroutine _playingCoroutine;

    public void InitializeSound(SoundData data){}
    public void Play(){}
    public void Stop(){}

}
