using UnityEngine;

namespace TerrorConsole
{
    public class HidingPlace : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _playerSprite;
        private bool _isPlayerInRange = false;
        [SerializeField] private GameObject _player;


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _player = other.gameObject;
                _isPlayerInRange = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _isPlayerInRange = false;
                _player = null;
            }
        }

        void Update()
        {
            if (_isPlayerInRange && Input.GetKeyDown(KeyCode.E))
            {                
                if (_playerSprite != null) 
                {
                    _playerSprite.enabled = !_playerSprite.enabled;
                    ChangeLayer(_player);
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
