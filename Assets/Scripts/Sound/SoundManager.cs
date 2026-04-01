using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SoundManager : PersistentSingleton<SoundManager>
{
    IObjectPool<SoundEmitter> _soundPool;

    protected override void Awake()
    {
        base.Awake();
    }
}