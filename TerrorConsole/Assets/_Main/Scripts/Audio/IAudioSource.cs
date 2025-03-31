using UnityEngine;

namespace TerrorConsole
{
    public interface IAudioSource
    {
        float SFXVolume { get; }
        float MusicVolume { get; }
        void SetSFXVolume(float newVolume);
        void SetMusicVolume(float newVolume);
        void PlayDoorCloseSFX();
        void PlayDoorOpenSFX();
        void PlayPauseSFX();
        void PlayUIButtonClickSFX();
        void PlayMusicPiano();
        void PlayShadowMusic();
        void PlayMysteryMusic();
        void PlayScaryBellsMusic();
        void PlayVoicesMusic();
        void StopMusic();
    }
}
