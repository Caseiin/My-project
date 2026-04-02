using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SoundManager : PersistentSingleton<SoundManager>
{
    IObjectPool<SoundEmitter> _soundEmitterPool;
    readonly List<SoundEmitter> _activeSoundEmitters = new();
    public readonly Dictionary<SoundData, int> Counts = new();

    [SerializeField] SoundEmitter soundEmitterPrefab;
    [SerializeField] bool collectionCheck = true;
    [SerializeField] int defaultSoundCapacity = 10;
    [SerializeField] int maxSoundCapacity = 30;


    void Start()
    {
        InitializePool();
    }

    public SoundBuilder CreateSound() => new SoundBuilder(this);

    public SoundEmitter Get()
    {
        return _soundEmitterPool.Get();
    }

    public void ReturnToPool(SoundEmitter emitter)
    {
        _soundEmitterPool.Release(emitter);
    }

    private void InitializePool()
    {
        _soundEmitterPool = new ObjectPool<SoundEmitter>(CreateSoundEmitter, OnTakefromPool, OnReturnToPool, OnDestroyFromPool,collectionCheck,defaultSoundCapacity,maxSoundCapacity
);
    }

    private void OnDestroyFromPool(SoundEmitter emitter)
    {
        Destroy(emitter.gameObject);
    }

    private void OnReturnToPool(SoundEmitter emitter)
    {
        if (Counts.TryGetValue(emitter.Data, out var count))
        {
            Counts[emitter.Data] = Mathf.Max(0, count - 1);
        }

        emitter.gameObject.SetActive(false);
        _activeSoundEmitters.Remove(emitter);
    }

    private void OnTakefromPool(SoundEmitter emitter)
    {
        emitter.gameObject.SetActive(true);
        _activeSoundEmitters.Add(emitter);
    }

    private SoundEmitter CreateSoundEmitter()
    {
        var soundEmitter = Instantiate(soundEmitterPrefab);
        soundEmitter.gameObject.SetActive(false);
        return soundEmitter;
    }


    // Helper method for SoundData count managment
    public bool CanPlaySound(SoundData data)
    {
        return !(Counts.TryGetValue(data, out var count)) || count < maxSoundCapacity;
    }
}