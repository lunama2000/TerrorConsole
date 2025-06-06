using UnityEngine;

namespace TerrorConsole
{
    public class MusicChangeOnStart : MonoBehaviour
    {
        [SerializeField] private string _musicKey;
        private void Start()
        {
            AudioManager.Source.PlayMusic(_musicKey);
        }

        public void PlayMusicUsingKey(string newMusicKey)
        {
            AudioManager.Source.PlayMusic(newMusicKey);
        }
    }
}
