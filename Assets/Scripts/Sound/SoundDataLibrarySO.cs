using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundLibrary", menuName ="Sound/Library")]
public class SoundDataLibrarySO : ScriptableObject
{
    [SerializeField] List<SoundData> sounds;
    public Dictionary<string, SoundData> soundLibrary = new();

    public void Initialize()
    {
        foreach (var sound in sounds)
        {
            soundLibrary[sound.name] = sound;

            if (soundLibrary.ContainsKey(sound.name))
            {
                Debug.LogWarning($"Duplicate sound name: {sound.name}");
            }
        }
    }
}
