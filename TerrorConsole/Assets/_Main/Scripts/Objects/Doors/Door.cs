using UnityEngine;
using UnityEngine.Events;

namespace TerrorConsole
{
    public class Door : MonoBehaviour
    {
        [SerializeField] protected bool _isLocked;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite openSprite;
        [SerializeField] private Sprite closedSprite;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private LevelEventsRecorder _eventRecorder;
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
            _spriteRenderer.sprite = openSprite;//TO DO Implement animation of door opening
            _collider2D.enabled = false;
        }

        public void CloseDoor()
        {
            _onDoorClosed?.Invoke();
            CloseDoorInmediate();
        }

        private void CloseDoorInmediate()
        {
            _spriteRenderer.sprite = closedSprite;//TO DO Implement animation of door opening
            _collider2D.enabled = true;
        }
    }
}
