using UnityEngine;

namespace TerrorConsole
{
    public class CapturePlayer : MonoBehaviour
    {
        [SerializeField] Collider2D ShadowCollider;

        public void ActivateCollider()
        {
            ShadowCollider.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                LevelManager.Source.PlayerCaptured();
            }
        }
    }
}