using UnityEngine;

namespace TerrorConsole
{
    public class InputManager : Singleton<IInputSource>, IInputSource
    {
        public bool ActionButton1 { get; private set; }
        public bool IsMoving { get; private set; }
        public Vector2 MovementDirection => _movementDirection.normalized;

        private Vector2 _movementDirection = Vector2.zero;
        
        private void Update()
        {
            SetMovementInput();
            SetActionButtons();
        }
        
        private void SetMovementInput()
        {
            if (Input.GetKey(KeyCode.D))
            {
                _movementDirection.x = 1;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                _movementDirection.x = -1;
            }
            else
            {
                _movementDirection.x = 0;
            }

            if (Input.GetKey(KeyCode.W))
            {
                _movementDirection.y = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _movementDirection.y = -1;
            }
            else
            {
                _movementDirection.y = 0;
            }
            
            IsMoving = _movementDirection is not { x: 0, y: 0 };
        }

        private void SetActionButtons()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                ActionButton1 = true;
            }
            else
            {
                ActionButton1 = false;
            }
        }
    }
}
