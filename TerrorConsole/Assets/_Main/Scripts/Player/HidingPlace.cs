using UnityEngine;

namespace TerrorConsole
{
    public class HidingPlace : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _playerSprite;
        [SerializeField] private Collider2D _playerCollider;
        private bool _isPlayerInRange = false; 


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _isPlayerInRange = true;
                Debug.Log("Colisión detectada con " + other.gameObject.name);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _isPlayerInRange = false;
                Debug.Log("El jugador salió del rango.");
            }
        }

        void Update()
        {
            if (_isPlayerInRange && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Escondite");

                if (_playerSprite != null) 
                {
                    _playerCollider.enabled = !_playerCollider.enabled;
                    _playerSprite.enabled = !_playerSprite.enabled;
                }
                    
            }

            /*if (_player.enabled = false && Input.GetKeyDown(KeyCode.E))
            {
                if (_player != null)
                {
                    _player.enabled = true;
                }

            }*/
        }
    }
}