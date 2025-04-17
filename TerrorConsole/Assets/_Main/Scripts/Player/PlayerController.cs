using UnityEngine;

namespace TerrorConsole
{
    public class PlayerController : MonoBehaviour
    {
        [Header("COMPONENTS")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;

        [Header("CONFIGURATIONS")]
        [SerializeField] private int _velocity = 10;
        private bool _freezeInput = false;

        private IInputSource _inputSource;

        private void Start()
        {
            _inputSource = InputManager.Source;
            LevelManager.Source.OnLevelStateChange += OnLevelStateChange;
        }

        private void OnDestroy()
        {
            LevelManager.Source.OnLevelStateChange -= OnLevelStateChange;
        }

        private void FixedUpdate()
        {
            if (!_freezeInput)
            {
                _animator.SetBool("isWalking", _inputSource.IsMoving);
            }

            if (!_inputSource.IsMoving || _freezeInput) return;
            
            _rigidbody.MovePosition((Vector2)transform.position + _inputSource.MovementDirection * (_velocity * Time.fixedDeltaTime));
            _animator.SetFloat("X", _inputSource.MovementDirection.normalized.x);
            _animator.SetFloat("Y", _inputSource.MovementDirection.normalized.y);
        }

        private void OnLevelStateChange(LevelState newState)
        {
            switch (newState)
            {
                case LevelState.InDialogue:
                case LevelState.Pause:
                case LevelState.Cinematic:
                    StopInput();
                    break;

                case LevelState.Play:
                    ResumeInput();
                    break;
            }
        }

        private void StopInput()
        {
            _freezeInput = true;
            _animator.SetBool("isWalking", false);

        }

        private void ResumeInput()
        {
            _freezeInput = false;
        }
        public void SetMovementEnabled(bool enabled)
        {
            _freezeInput = !enabled;
            _animator.SetBool("isWalking", false);
        }
    }
}
