using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Rendering.DebugUI;

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

        private void InitializeSFX()
        {
            SFXVolume = SaveSystemManager.Source.GetSavedSFXVolume();
            _sfxMixer.SetFloat("sfxVol", Mathf.Lerp(-80f, 0f, SFXVolume));
        }

        private void InitializeMusic()
        {
            MusicVolume = SaveSystemManager.Source.GetSavedMusicVolume();
            _musicMixer.SetFloat("musicVol", Mathf.Lerp(-80f, 0f, MusicVolume));
        }

        public void PlaySFX(string audioKey)
        {
            var audioData = _audioDatabase.GetAudio(audioKey);
            if (audioData == null)
            {
                Debug.LogWarning($"[AudioManager] SFX '{audioKey}' not found in AudioDatabase.");
                return;
            }

            _sfxAudioSource.PlayOneShot(audioData.AudioClip, audioData.Volume);
        }

        public void PlayMusic(string audioKey)
        {
            var audioData = _audioDatabase.GetAudio(audioKey);
            if (audioData == null)
            {
                Debug.LogWarning($"[AudioManager] Music '{audioKey}' not found in AudioDatabase.");
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

        public void SetSFXVolume(float newVolume)
        {
            SFXVolume = Mathf.Clamp01(newVolume);
            var dB = Mathf.Lerp(-20f, 15f, SFXVolume);
            if (dB < -19)
                dB = -80;
            _sfxMixer.SetFloat("sfxVol", dB);
            SaveSystemManager.Source.SaveSFXVolume(SFXVolume);
        }

        public void SetMusicVolume(float newVolume)
        {
            MusicVolume = Mathf.Clamp01(newVolume);
            var dB = Mathf.Lerp(-20f, 15f, MusicVolume);
            if (dB < -19)
                dB = -80;
            _musicMixer.SetFloat("musicVol", dB);
            SaveSystemManager.Source.SaveMusicVolume(MusicVolume);
        }
    }
}
