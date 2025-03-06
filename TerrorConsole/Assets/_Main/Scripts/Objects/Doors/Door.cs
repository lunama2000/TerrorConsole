using UnityEngine;

namespace TerrorConsole
{
    public class Door : MonoBehaviour
    {
        [SerializeField] protected bool _isLocked;

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
            GetComponent<SpriteRenderer>().color = new Color(0.5f,0.5f,0.5f,0.5f);//TO DO Implement animation of door opening
            GetComponent<Collider2D>().enabled = false;
        }

        protected virtual void CloseDoor()
        {
            GetComponent<SpriteRenderer>().color = Color.red;//TO DO Implement animation of door opening
            GetComponent<Collider2D>().enabled = true;
        }
    }
}
