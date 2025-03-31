using System;
using UnityEngine;

namespace TerrorConsole
{
    public class AudioStartTest : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.Source.PlayMysteryMusic();
        }
    }
}
