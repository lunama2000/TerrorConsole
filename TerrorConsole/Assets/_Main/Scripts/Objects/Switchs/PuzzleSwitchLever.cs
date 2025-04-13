using UnityEngine;

namespace TerrorConsole
{
    public class PuzzleSwitchLever : SwitchObject
    {
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

        protected override void AlternateState()
        {
            _state = !_state;
            if (_state)
            {
                OnActivated?.Invoke(_id);
                CameraSystemManager.Source.ShakeCamera();
            }
            else
            {
                OnDeactivated?.Invoke(_id);
            }
        }
    }
}
