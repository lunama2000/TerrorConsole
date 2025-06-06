using System;
using UnityEngine;

namespace TerrorConsole
{
    public class CapturePlayer : MonoBehaviour
    {
        [SerializeField] Collider2D ShadowCollider;
        [SerializeField] private string _sfxCapturedKey;

        public void ActivateCollider()
        {
            ShadowCollider.enabled = true;
        }

        public void Capture()
        {
            if (!string.IsNullOrEmpty(_sfxCapturedKey))
            {
                AudioManager.Source.PlaySFX( _sfxCapturedKey );
            }
            LevelManager.Source.PlayerCaptured();
            CameraSystemManager.Source.ShakeCamera();
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