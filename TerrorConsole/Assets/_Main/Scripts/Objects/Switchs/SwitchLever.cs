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
                InputManager.Source.OnActivateButton1 += AlternateState;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                InputManager.Source.OnActivateButton1 -= AlternateState;

            }
        }
    }
}
