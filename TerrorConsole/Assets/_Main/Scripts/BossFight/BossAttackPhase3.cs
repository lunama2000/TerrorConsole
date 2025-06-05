using UnityEngine;

namespace TerrorConsole
{
    public class BossAttackPhase3 : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float lifetime = 3f;

        private BossfightPhaseThree _controller;
        private Rigidbody2D _rigidbody;
        private Vector2 _moveDirection;

        private bool _isActive = false;
        private bool _isPaused = false;

        public void Setup(BossfightPhaseThree controller, Transform target)
        {
            _controller = controller;
            _rigidbody = GetComponent<Rigidbody2D>();

            _moveDirection = (target.position - transform.position).normalized;

            _isActive = true;
            _isPaused = false;

            Destroy(gameObject, lifetime);
            
            LevelManager.Source.OnLevelStateChange += HandleLevelStateChange;
        }

        private void OnDestroy()
        {
            if (LevelManager.Source != null)
                LevelManager.Source.OnLevelStateChange -= HandleLevelStateChange;
        }

        private void HandleLevelStateChange(LevelState newState)
        {
            _isPaused = newState != LevelState.BossPhase;
        }

        private void FixedUpdate()
        {
            if (!_isActive || _isPaused || _rigidbody == null)
                return;

            _rigidbody.MovePosition(_rigidbody.position + _moveDirection * moveSpeed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Flashlight"))
            {
                _controller.RegisterLightHit();
                Destroy(gameObject);
            }
        }
    }
}
