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
    }
}
