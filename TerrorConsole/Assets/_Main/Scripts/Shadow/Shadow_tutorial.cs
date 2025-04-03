using UnityEngine;
using UnityEngine.Events;

namespace TerrorConsole
{
    // This script is only used once in the tutorial for the first appearance of the shadow
    public class Shadow_tutorial : MonoBehaviour
    {
        [SerializeField] Animator _appearance;
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
            _appearance.Play("Firts appearance");
        }

        private void disappearance() //Here i deactive the sahdow using it as an event when the animation ends
        {
            _shadow.gameObject.SetActive(false);
        }
    }
}