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

        public float SFXVolume { get; private set; }

        private void Start()
        {
            InitializeSFX();
        }
        private void InitializeSFX()
        {
            SFXVolume = PlayerPrefs.GetFloat("sfxVol", SFXVolume);
            _sfxMixer.SetFloat("sfxVol", SFXVolume);
        }
        public void PlayDoorCloseSFX()
        {
            AudioData audioData = _audioDatabase.GetAudio("DoorCloseSFX");
            _sfxAudioSource.PlayOneShot(audioData.audioClip, audioData.volume);
        }

        public void PlayDoorOpenSFX()
        {
            AudioData audioData = _audioDatabase.GetAudio("DoorOpenSFX");
            _sfxAudioSource.PlayOneShot(audioData.audioClip, audioData.volume);
        }

        public void PlayPauseSFX()
        {
            AudioData audioData = _audioDatabase.GetAudio("PauseSFX");
            _sfxAudioSource.PlayOneShot(audioData.audioClip, audioData.volume);
        }

        public void PlayUIButtonClickSFX()
        {
            AudioData audioData = _audioDatabase.GetAudio("UIButtonClickSFX");
            _sfxAudioSource.PlayOneShot(audioData.audioClip, audioData.volume);
        }

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
    }
}
