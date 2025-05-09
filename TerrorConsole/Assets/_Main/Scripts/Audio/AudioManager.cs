using UnityEngine;
using UnityEngine.Audio;

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
            _sfxMixer.SetFloat("sfxVol", Mathf.Lerp(-80f, 20f, SFXVolume));
            Debug.Log($"[AudioManager] Init SFX Volume: {SFXVolume}");
        }

        private void InitializeMusic()
        {
            MusicVolume = SaveSystemManager.Source.GetSavedMusicVolume();
            _musicMixer.SetFloat("musicVol", Mathf.Lerp(-80f, 20f, MusicVolume));
            Debug.Log($"[AudioManager] Init Music Volume: {MusicVolume}");
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
            newVolume = Mathf.Clamp01(newVolume);
            SFXVolume = newVolume;
            _sfxMixer.SetFloat("sfxVol", Mathf.Lerp(-80f, 20f, newVolume));
            Debug.Log("[AudioManager] Music volume (dB): " + Mathf.Lerp(-80f, 20f, SFXVolume));
            SaveSystemManager.Source.SaveSFXVolume(newVolume);
        }

        public void SetMusicVolume(float newVolume)
        {
            newVolume = Mathf.Clamp01(newVolume);
            MusicVolume = newVolume;
            _musicMixer.SetFloat("musicVol", Mathf.Lerp(-80f, 20f, newVolume));
            Debug.Log("[AudioManager] Music volume (dB): " + Mathf.Lerp(-80f, 20f, MusicVolume));
            SaveSystemManager.Source.SaveMusicVolume(newVolume);
        }
    }
}
