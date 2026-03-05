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


    private void Awake(){}
    private void Start(){}
    private void Update(){}

    #region Initialization

    private void InitializeDictionary()
    {
        foreach (SoundData soundData in _soundDataLibrary)
        {
            if (!_soundDictionary.ContainsKey(soundData.Type))
            {
                _soundDictionary[soundData.Type] = new List<SoundData>();
            }

            _soundDictionary[soundData.Type].Add(soundData);
        }
    }

    private void ConfigureAudioSources()
    {
        if (_musicSource != null)
            _musicSource.loop = true;

        if (_sfxSource != null)
            _sfxSource.loop = false;

        if (_loopedSfxSource != null)
            _loopedSfxSource.loop = true;
    }

    #endregion

    #region Music

    public void PlayMusic(SoundType type, bool reset = false)
    {
        if (!_soundDictionary.ContainsKey(type) || _musicSource == null)
            return;

        var sounds = _soundDictionary[type];
        if (sounds.Count == 0)
            return;

        var selected = GetRandomSound(sounds);

        if (reset)
            _musicSource.Stop();

        if (_musicSource.clip == selected.Clip && _musicSource.isPlaying)
            return;

        _musicSource.clip = selected.Clip;
        _musicSource.Play();
    }

    public void StopMusic()
    {
        if (_musicSource != null)
            _musicSource.Stop();
    }

    #endregion

    #region One-Shot SFX

    public void PlaySFX(SoundType type)
    {
        if (!_soundDictionary.ContainsKey(type) || _sfxSource == null)
            return;

        var sounds = _soundDictionary[type];
        if (sounds.Count == 0)
            return;

        var selected = GetRandomSound(sounds);

        _sfxSource.PlayOneShot(selected.Clip);
    }

    #endregion

    #region Looped SFX (Skating, Jetpack, etc.)

    public void StartLoopedSFX(SoundType type)
    {
        if (!_soundDictionary.ContainsKey(type) || _loopedSfxSource == null)
            return;

        var sounds = _soundDictionary[type];
        if (sounds.Count == 0)
            return;

        var selected = GetRandomSound(sounds);

        if (_loopedSfxSource.clip == selected.Clip && _loopedSfxSource.isPlaying)
            return;

        _loopedSfxSource.clip = selected.Clip;
        _loopedSfxSource.Play();
    }

    public  void StopLoopedSFX()
    {
        if (_loopedSfxSource != null)
            _loopedSfxSource.Stop();
    }

    #endregion

    #region Helpers

    private SoundData GetRandomSound(List<SoundData> sounds)
    {
        return sounds[Random.Range(0, sounds.Count)];
    }

    #endregion
}