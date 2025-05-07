using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Windows;

namespace TerrorConsole
{
    public class AudioManager : Singleton<IAudioSource>, IAudioSource
    {
        [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioMixer _sfxMixer;
    [SerializeField] private AudioMixer _musicMixer;
    [SerializeField] private AudioDatabase _audioDatabase;

    private AudioClip _currentMusic;

    public float SFXVolume { get; private set; }
    public float MusicVolume { get; private set; }

    private void Start()
    {
        InitializeSFX();
        InitializeMusic();
    }

    #region Initialization

    private void InitializeSFX()
    {
        SFXVolume = PlayerPrefs.GetFloat("sfxVol", 0.5f);
        _sfxMixer.SetFloat("sfxVol", Mathf.Lerp(-80f, 20f, SFXVolume));
    }

    private void InitializeMusic()
    {
        MusicVolume = PlayerPrefs.GetFloat("musicVol", 0.5f);
        _musicMixer.SetFloat("musicVol", Mathf.Lerp(-80f, 20f, MusicVolume));
    }

    #endregion

    #region Generalized Audio Play Methods

    public void PlaySFX(string audioKey)
    {
        AudioData audioData = _audioDatabase.GetAudio(audioKey);
        if (audioData == null)
        {
            Debug.LogWarning($"[AudioManager] No SFX found for key: {audioKey}");
            return;
        }

        _sfxAudioSource.PlayOneShot(audioData.AudioClip, audioData.Volume);
    }

    public void PlayMusic(string musicKey)
    {
        AudioData audioData = _audioDatabase.GetAudio(musicKey);
        if (audioData == null)
        {
            Debug.LogWarning($"[AudioManager] No Music found for key: {musicKey}");
            return;
        }

        if (_currentMusic == audioData.AudioClip) return;

        _currentMusic = audioData.AudioClip;
        _musicAudioSource.clip = _currentMusic;
        _musicAudioSource.loop = true;
        _musicAudioSource.Play();
    }

    public void StopMusic()
    {
        _musicAudioSource.Stop();
        _musicAudioSource.clip = null;
        _currentMusic = null;
    }

    #endregion

    #region Volume Control

    public void SetSFXVolume(float newVolume)
    {
        newVolume = Mathf.Clamp01(newVolume);
        SFXVolume = newVolume;
        _sfxMixer.SetFloat("sfxVol", Mathf.Lerp(-80f, 20f, newVolume));
        PlayerPrefs.SetFloat("sfxVol", newVolume);
    }

    public void SetMusicVolume(float newVolume)
    {
        newVolume = Mathf.Clamp01(newVolume);
        MusicVolume = newVolume;
        _musicMixer.SetFloat("musicVol", Mathf.Lerp(-80f, 20f, newVolume));
        PlayerPrefs.SetFloat("musicVol", newVolume);
    }

    #endregion

    #region (Optional) Legacy SFX Wrappers

    public void PlayDoorCloseSFX() => PlaySFX("DoorCloseSFX");
    public void PlayDoorOpenSFX() => PlaySFX("DoorOpenSFX");
    public void PlayPauseSFX() => PlaySFX("PauseSFX");
    public void PlayUIButtonClickSFX() => PlaySFX("UIButtonClickSFX");

    #endregion
    }
}
