using System;
using UnityEngine;

namespace TerrorConsole
{
    public class AudioMusicStart : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.Source.PlayMusicPiano();
        }
    }
}
