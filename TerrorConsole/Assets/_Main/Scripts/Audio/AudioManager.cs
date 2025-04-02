using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Windows;

namespace TerrorConsole
{
    public class AudioManager : Singleton<IAudioSource>, IAudioSource
    {
        [SerializeField] private AudioSource _sfxAudioSource;
        [SerializeField] private AudioDatabase _audioDatabase;
        [SerializeField] private AudioMixer _sfxMixer;
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioMixer _musicMixer;
        
        private AudioClip _currentMusic;

        public float SFXVolume { get; private set; }
        public float MusicVolume { get; private set; }

        private void Start()
        {
            InitializeSFX();
            InitializeMusic();
        }
        
        #region SFX
        private void InitializeSFX()
        {
            SFXVolume = PlayerPrefs.GetFloat("sfxVol", SFXVolume);
            _sfxMixer.SetFloat("sfxVol", SFXVolume);
        }

        public void PlayDoorCloseSFX()
        {
            PlayOneShotSFX("DoorCloseSFX");
        }

        public void PlayDoorOpenSFX()
        {
            PlayOneShotSFX("DoorOpenSFX");
        }

        public void PlayPauseSFX()
        {
            PlayOneShotSFX("PauseSFX");
        }

        public void PlayUIButtonClickSFX()
        {
            PlayOneShotSFX("UIButtonClickSFX");
        }

        private void PlayOneShotSFX(string audioName)
        {
            AudioData audioData = _audioDatabase.GetAudio(audioName);
            _sfxAudioSource.PlayOneShot(audioData.AudioClip, audioData.Volume);
        }
        
        #endregion
        
        #region Music

        private void InitializeMusic()
        {
            MusicVolume = PlayerPrefs.GetFloat("musicVol", MusicVolume);
            _musicMixer.SetFloat("musicVol", MusicVolume);
        }
        
        public void PlayMusic(MusicType musicType)
        {
            string audioName = GetMusicName(musicType);
            
            AudioData audioData = _audioDatabase.GetAudio(audioName);
            if (audioData == null) return; 

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
        /// <summary>
        /// New volume represented in a range from 0 to 1
        /// </summary>
        /// <param name="newVolume"></param>
        public void SetSFXVolume(float newVolume)
        {
            newVolume = Mathf.Clamp(newVolume, 0f, 1f);
            SFXVolume = Mathf.Lerp(-80f, 20f, newVolume);
            _sfxMixer.SetFloat("sfxVol", SFXVolume);
            PlayerPrefs.SetFloat("sfxVol", SFXVolume);
        }
        
        /// <param name="newVolume"></param>
        public void SetMusicVolume(float newVolume)
        {
            newVolume = Mathf.Clamp(newVolume, 0f, 1f);
            MusicVolume = Mathf.Lerp(-80f, 20f, newVolume);
            _musicMixer.SetFloat("musicVol", MusicVolume);
            PlayerPrefs.SetFloat("musicVol", MusicVolume);
        }

        private string GetMusicName(MusicType musicType)
        {
            switch (musicType)
            {
                case MusicType.LoudTones: return "LoudTonesMusic";
                case MusicType.Voices: return "VoicesMusic";
                case MusicType.MusicPiano: return "MusicPiano";
                case MusicType.Mystery: return "MysteryMusic";
                case MusicType.Shadow: return "ShadowMusic";
                case MusicType.ScaryBells: return "ScaryBells";
                default: return null;
            }
        }
    }
}
