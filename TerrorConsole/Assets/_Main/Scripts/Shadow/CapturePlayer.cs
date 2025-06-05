using System;
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

        public void Capture()
        {
            LevelManager.Source.PlayerCaptured();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Capture();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Capture();
            }
        }
    }
}