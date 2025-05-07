using UnityEngine;

namespace TerrorConsole
{
    public interface IAudioSource
    {
        float SFXVolume { get; }
        float MusicVolume { get; }
    
        void SetSFXVolume(float newVolume);
        void SetMusicVolume(float newVolume);

        void PlaySFX(string audioKey);
        void PlayMusic(string musicKey);
        void StopMusic();
        
    }
}
