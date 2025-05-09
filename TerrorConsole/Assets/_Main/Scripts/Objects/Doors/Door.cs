using UnityEngine;
using UnityEngine.Events;

namespace TerrorConsole
{
    public class Door : MonoBehaviour
    {
        [SerializeField] protected bool _isLocked;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private LevelEventsRecorder _eventRecorder;
        [SerializeField] private Animator _animator;
        [SerializeField] private UnityEvent _onDoorOpened = new UnityEvent();
        [SerializeField] private UnityEvent _onDoorClosed = new UnityEvent();

        private void Start()
        {
            if (_eventRecorder.CheckEventState())
            {
                OpenDoorInmediate();
            }
            else
            {
                CloseDoorInmediate();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerCollisioned();
            }
        }

        protected virtual void PlayerCollisioned()
        {
            if (!_isLocked)
            {
                OpenDoor();
            }
        }

        public void UnlockDoor()
        {
            _isLocked = false;
        }
        
        public void LockDoor()
        {
            _isLocked = true;
        }

        public void OpenDoor()
        {
            UnlockDoor();
            _onDoorOpened?.Invoke();
            OpenDoorInmediate();
        }

        private void OpenDoorInmediate()
        {
            _animator.SetTrigger("Open");
            _collider2D.enabled = false;
        }

        public void CloseDoor()
        {
            _onDoorClosed?.Invoke();
            CloseDoorInmediate();
        }

        private void CloseDoorInmediate()
        {
            _animator.SetTrigger("Close");
            _collider2D.enabled = true;
        }
    }
}
