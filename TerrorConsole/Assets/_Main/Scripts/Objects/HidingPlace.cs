using UnityEngine;

namespace TerrorConsole
{
    public class HidingPlace : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        private bool _isHiding = false;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                InputManager.Source.OnActivateButton2 += ToggleHiding;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                InputManager.Source.OnActivateButton2 -= ToggleHiding;
            }
        }

        private void ToggleHiding()
        {
            _isHiding = !_isHiding;
            if (_isHiding)
            {
                _playerController.Hide();
            }
            else
            {
                _playerController.UnHide();
            }
        }
    }
}
