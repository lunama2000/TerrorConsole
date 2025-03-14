using UnityEngine;

namespace TerrorConsole
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/AudioData")]

    public class AudioData : ScriptableObject
    {
        public string audioName;
        public AudioClip audioClip;
        public float volume = 1;
    }
}
