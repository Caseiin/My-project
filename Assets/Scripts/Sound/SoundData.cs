using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundData
{
    public AudioClip Clip;
    public AudioMixerGroup mixerGroup;
    public bool Loop;
    public bool PlayonAwake;
}
