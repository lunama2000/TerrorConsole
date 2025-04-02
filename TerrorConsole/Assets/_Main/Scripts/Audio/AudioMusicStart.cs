using System;
using UnityEngine;

namespace TerrorConsole
{
    public class AudioMusicStart : MonoBehaviour
    {
        [SerializeField] private MusicType musicType;
        private void Start()
        {
            AudioManager.Source.PlayMusic(musicType);
        }
    }
}
