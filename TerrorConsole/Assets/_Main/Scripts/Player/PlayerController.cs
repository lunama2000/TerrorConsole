using UnityEngine;

namespace TerrorConsole
{
    public class PlayerController : MonoBehaviour
    {
        [Header("COMPONENTS")]
        [SerializeField] private Rigidbody2D _rigidbody;
        
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
            if (!_inputSource.IsMoving || _freezeInput) return;
            
            _rigidbody.MovePosition((Vector2)transform.position + _inputSource.MovementDirection * (_velocity * Time.fixedDeltaTime));
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
        }

        private void ResumeInput()
        {
            _freezeInput = false;
        }
    }
}
