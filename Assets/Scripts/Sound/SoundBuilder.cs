using UnityEngine;

public class SoundBuilder
{
    readonly SoundManager _soundManager;
    Vector3 position = Vector3.zero;

    public SoundBuilder(SoundManager manager)
    {
        _soundManager = manager;
    }

    public SoundBuilder WithPosition(Vector3 position)
    {
        this.position = position;
        return this;
    }
}
