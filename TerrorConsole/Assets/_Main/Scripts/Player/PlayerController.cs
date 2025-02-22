using UnityEngine;

namespace TerrorConsole
{
    public class PlayerController : MonoBehaviour
    {
        [Header("COMPONENTS")]
        [SerializeField] private Rigidbody2D _rigidbody;
        
        [Header("CONFIGURATIONS")]
        [SerializeField] private int _velocity = 10;

        private IInputSource _inputSource;

        private void Start()
        {
            _inputSource = InputManager.Source;
        }

        private void FixedUpdate()
        {
            if (!_inputSource.IsMoving) return;
            
            _rigidbody.MovePosition((Vector2)transform.position + _inputSource.MovementDirection * (_velocity * Time.fixedDeltaTime));
        }
    }
}
