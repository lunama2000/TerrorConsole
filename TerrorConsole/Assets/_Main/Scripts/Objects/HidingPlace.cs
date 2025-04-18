using UnityEngine;

namespace TerrorConsole
{
    public class HidingPlace : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        private bool _isPlayerInRange = false;
        private bool _isHiding = false;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _isPlayerInRange = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _isPlayerInRange = false;
                _playerController = null;
            }
        }

        void Update()
        {
            Hiding();
        }

        private void Hiding()
        {
            if (_isPlayerInRange && Input.GetKeyDown(KeyCode.E))
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
}
