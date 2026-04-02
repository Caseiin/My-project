using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName ="Sound",menuName ="Sound/Data")]
public class SoundData : ScriptableObject
{
    public AudioClip Clip;
    public AudioMixerGroup mixerGroup;
    public bool Loop;
    public bool PlayonAwake;
}
