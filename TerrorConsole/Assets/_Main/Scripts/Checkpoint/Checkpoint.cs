using UnityEngine;

namespace TerrorConsole
{
    public class Checkpoint : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            LevelManager.Source.SetRespawnPosition(transform.position);
        }
    }
}
