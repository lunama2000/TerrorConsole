using UnityEngine;
using static TerrorConsole.LevelManager;

namespace TerrorConsole
{
    public class PlayerController : MonoBehaviour
    {
        [Header("COMPONENTS")]
        [SerializeField] private Rigidbody2D _rigidbody;
        
        [Header("CONFIGURATIONS")]
        [SerializeField] private int _velocity = 10;
        [SerializeField] private bool _freezeInput = false;

        private IInputSource _inputSource;

        private void Start()
        {
            _inputSource = InputManager.Source;
        }
        private void OnEnable()
        {
            LevelManager.Source.OnLevelStateChange += OnLevelStateChange;
        }

        private void FixedUpdate()
        {
            if (!_inputSource.IsMoving || _freezeInput) return;
            
            _rigidbody.MovePosition((Vector2)transform.position + _inputSource.MovementDirection * (_velocity * Time.fixedDeltaTime));
        }

        private void OnLevelStateChange(LevelState newState)
        {
            if(newState == LevelState.InDialogue || newState == LevelState.Pause || newState == LevelState.Cinematic)
            {
                _freezeInput = true;
            }
            else
            {
                _freezeInput = false;
            }
        }
    }
}
