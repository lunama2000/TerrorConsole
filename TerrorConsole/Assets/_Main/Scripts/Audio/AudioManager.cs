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
