using System;
using UnityEngine;
using UnityEngine.Events;

namespace TerrorConsole
{
    public class TriggerEvents : MonoBehaviour
    {
        [SerializeField] private LevelEventsRecorder _levelEventsRecorder;
        [SerializeField] private UnityEvent _onTriggerEnter = new UnityEvent();
        [SerializeField] private String _sfxTriggerKey;

        private void Start()
        {
            if (_levelEventsRecorder.CheckEventState())
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _onTriggerEnter?.Invoke();
            }
        }

        public void PlayTriggerSound()
        {
            AudioManager.Source.PlaySFX(_sfxTriggerKey);
        }

    }
}
