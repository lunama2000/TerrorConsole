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
                OpenDoor();
            }
            else
            {
                CloseDoor();
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

        protected virtual void UnlockDoor()
        {
            _isLocked = false;
        }
        protected virtual void LockDoor()
        {
            _isLocked = true;
        }

        protected virtual void OpenDoor(bool instantly = false)
        {
            if (!instantly)
            {
                //Place here SFX or effects
            }
            _spriteRenderer.sprite = openSprite;//TO DO Implement animation of door opening
            _eventRecorder.RegisterLevelEvent(true);
            _collider2D.enabled = false;
            _onDoorOpened?.Invoke();
        }

        protected virtual void CloseDoor(bool instantly = false)
        {
            if (!instantly)
            {
                //Place here SFX or effects
            }
            _spriteRenderer.sprite = closedSprite;//TO DO Implement animation of door opening
            _eventRecorder.RegisterLevelEvent(false);
            _collider2D.enabled = true;
            _onDoorClosed?.Invoke();
        }
    }
}
