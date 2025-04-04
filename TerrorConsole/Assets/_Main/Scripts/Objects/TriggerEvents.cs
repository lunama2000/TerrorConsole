using UnityEngine;
using UnityEngine.Events;

namespace TerrorConsole
{
    public class TriggerEvents : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onTriggerEnter = new UnityEvent();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _onTriggerEnter?.Invoke();
            }
        }


    }
}
