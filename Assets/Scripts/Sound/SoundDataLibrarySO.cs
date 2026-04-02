using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundLibrary", menuName ="Sound/Library")]
public class SoundDataLibrarySO : ScriptableObject
{
    [SerializeField] List<SoundData> sounds;
    Dictionary<string, SoundData> _soundLibrary = new();

    public void Initialize()
    {
        foreach (var sound in sounds)
        {

            if (_soundLibrary.ContainsKey(sound.name))
            {
                Debug.LogWarning($"Duplicate sound name: {sound.name}");
                continue;
            }

            _soundLibrary[sound.name] = sound;
        }
    }

    public SoundData Get(string soundName)
    {
        if (_soundLibrary.TryGetValue(soundName, out var data)) return data;

        Debug.LogWarning($"Sound not found: {soundName}");
        return null;
    }
}
