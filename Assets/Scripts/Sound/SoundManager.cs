using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [Header("Sound Library")]
    [SerializeField] private SoundData[] _soundDataLibrary;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _loopedSfxSource;

    private Dictionary<SoundType, List<SoundData>> _soundDictionary = new();
}