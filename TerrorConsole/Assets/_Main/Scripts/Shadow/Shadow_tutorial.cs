using UnityEngine;
using UnityEngine.Events;

namespace TerrorConsole
{
    public class Shadow_tutorial : MonoBehaviour
    {
        [SerializeField] Animator _aparicion;
        [SerializeField] private GameObject _shadow;
        [SerializeField] private Collider2D _door;
        [SerializeField] private UnityEvent onDoorOpened; 

        private void Start()
        {
            if (onDoorOpened == null)
                onDoorOpened = new UnityEvent();
        }

        private void FixedUpdate()
        {
            if (_door.enabled == false)
            {
                TriggerAnimation();
            }
        }

        private void TriggerAnimation()
        {
            onDoorOpened.Invoke(); 
            _aparicion.Play("Primer aparición");
        }

        private void Desaparicion()
        {
            _shadow.gameObject.SetActive(false);
        }
    }
}