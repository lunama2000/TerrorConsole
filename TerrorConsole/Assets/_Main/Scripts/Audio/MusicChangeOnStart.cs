using UnityEngine;

namespace TerrorConsole
{
    public class MusicChangeOnStart : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.Source.PlayMusic("ScaryBells");
        }
    }
}
