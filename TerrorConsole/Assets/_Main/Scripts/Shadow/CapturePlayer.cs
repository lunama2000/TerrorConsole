using System;
using UnityEngine;
using UnityEngine.Events;

namespace TerrorConsole
{
    public class CapturePlayer : MonoBehaviour
    {
       [SerializeField] Collider2D ShadowCollider;
       public UnityEvent OnplayerCaptured;
       public UnityEvent OnplayerRespawn;

        public void ActivateCollider()
        {
            ShadowCollider.enabled = true;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                OnplayerCaptured?.Invoke();
            }
        }
    }
}
