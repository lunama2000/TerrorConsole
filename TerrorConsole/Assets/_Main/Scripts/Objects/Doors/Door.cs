using UnityEngine;

namespace TerrorConsole
{
    public class Door : MonoBehaviour
    {
        [SerializeField] protected bool _isLocked;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite newSprite;
        [SerializeField] private Collider2D _collider2D;

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
                return;
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

        protected virtual void OpenDoor()
        {
            _spriteRenderer.sprite = newSprite;//TO DO Implement animation of door opening
            _collider2D.enabled = false;
        }

        protected virtual void CloseDoor()
        {
            _spriteRenderer.color = Color.red;//TO DO Implement animation of door opening
            _collider2D.enabled = true;
        }
    }
}
