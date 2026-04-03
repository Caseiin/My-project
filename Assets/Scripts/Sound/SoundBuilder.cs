using UnityEngine;

public class SoundBuilder
{
    readonly SoundManager _soundManager;
    SoundData soundData;
    Vector3 position = Vector3.zero;
    Transform transform = null;
    bool randomPitch;

    public SoundBuilder(SoundManager manager)
    {
        _soundManager = manager;
    }

    public SoundBuilder WithSound(SoundData data)
    {
        soundData = data;
        return this;
    }

    public SoundBuilder WithPosition(Vector3 position)
    {
        this.position = position;
        return this;
    }

    public SoundBuilder WithParent(Transform transform)
    {
        this.transform = transform;
        return this;
    }

    public SoundBuilder WithRandomSound(bool pitch)
    {
        randomPitch = pitch;
        return this;
    }

    public SoundEmitter Play()
    {
        if(!_soundManager.CanPlaySound(soundData)) return null;

        var soundEmitter = _soundManager.Get();
        soundEmitter.InitializeSound(soundData);
        soundEmitter.transform.position = position;
        soundEmitter.transform.parent = transform;

        if (randomPitch)
        {
            soundEmitter.RandomizePitch();
        } 

        if(soundData.FrequentSound){
            SoundManager.Instance.FrequentSoundEmitters.Enqueue(soundEmitter);
        }

        soundEmitter.Play();
        return soundEmitter;
    }
}
