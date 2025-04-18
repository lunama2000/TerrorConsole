using UnityEngine;

namespace TerrorConsole
{
    public class HidingPlace : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _playerSprite;
        [SerializeField] private GameObject _player;

        private PlayerController _playerController;
        private bool _isPlayerInRange = false;
        private bool _isHiding = false;


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _player = other.gameObject;
                _playerController = _player.GetComponent<PlayerController>();
                _isPlayerInRange = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _isPlayerInRange = false;
                _player = null;
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
                if (_playerSprite != null)
                {
                    _isHiding = !_isHiding;
                    _playerSprite.enabled = !_playerSprite.enabled;
                    ChangeLayer(_player);
                }

                if (_playerController != null)
                {
                    _playerController.SetMovementEnabled(!_isHiding);
                }
            }
        }

        private void ChangeLayer(GameObject player)
        {
            if (player.layer == LayerMask.NameToLayer("Player"))
            {
                player.layer = LayerMask.NameToLayer("Default");
            }
            else if (player.layer == LayerMask.NameToLayer("Default"))
            {
                player.layer = LayerMask.NameToLayer("Player");
            }
        }
    }
}
