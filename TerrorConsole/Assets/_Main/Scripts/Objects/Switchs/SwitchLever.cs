using UnityEngine;

namespace TerrorConsole
{
    public class SwitchLever : SwitchObject
    {
        private bool _isPlayerColliding;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _isPlayerColliding = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _isPlayerColliding = false;
            }
        }

        private void Update()
        {
            if (!_isPlayerColliding)
            {
                return;
            }

            if (InputManager.Source.ActionButton1)
            {
                AlternateState();
            }
        }
    }
}
