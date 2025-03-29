using System;
using UnityEngine;

namespace TerrorConsole
{
    public class InputManager : Singleton<IInputSource>, IInputSource
    {
        public bool IsMoving { get; private set; }
        public Vector2 MovementDirection => _movementDirection.normalized;

        public Action OnActivateButton1 { get; set; }

        private Vector2 _movementDirection = Vector2.zero;
        
        private void Update()
        {
            SetMovementInput();
            SetActionButtons();
        }
        
        private void SetMovementInput()
        {
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                _movementDirection.x = 1;
            }
            else if (Input.GetAxisRaw("Horizontal") == -1)
            {
                _movementDirection.x = -1;
            }
            else
            {
                _movementDirection.x = 0;
            }

            if (Input.GetAxisRaw("Vertical") == 1)
            {
                _movementDirection.y = 1;
            }
            else if (Input.GetAxisRaw("Vertical") == -1)
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
            if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                OnActivateButton1?.Invoke();
            }
        }
    }
}
