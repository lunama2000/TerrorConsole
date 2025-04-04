using UnityEngine;

namespace TerrorConsole
{
    public class MusicChangeOnStart : MonoBehaviour
    {
        [SerializeField] private MusicType musicType;
        private void Start()
        {
            AudioManager.Source.PlayMusic(musicType);
        }
    }
}
