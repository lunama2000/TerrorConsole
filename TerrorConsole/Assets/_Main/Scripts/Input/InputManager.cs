using System;
using UnityEngine;

namespace TerrorConsole
{
    public class InputManager : Singleton<IInputSource>, IInputSource
    {
        [SerializeField] private float deadZone = 0.5f;

        public bool IsMoving { get; private set; }
        public Vector2 MovementDirection => _movementDirection.normalized;
        public Vector2 LastLookDirection => _lastLookDirection.normalized;

        public Action OnActivateButton1 { get; set; }
        public Action OnActivateButton2 { get; set; }

        private Vector2 _movementDirection = Vector2.zero;
        private Vector2 _lastLookDirection = Vector2.zero;

        private void Update()
        {
            SetMovementInput();
            SetActionButtons();
            SetLookDirection();
        }

        private void SetLookDirection()
        {
            if (_lastLookDirection == _movementDirection || _movementDirection == Vector2.zero)
                return;
            
            _lastLookDirection = _movementDirection;
        }

        private void SetMovementInput()
        {
            if (Input.GetAxisRaw("Horizontal") > deadZone)
            {
                _movementDirection.x = 1;
            }
            else if (Input.GetAxisRaw("Horizontal") < -(deadZone))
            {
                _movementDirection.x = -1;
            }
            else
            {
                _movementDirection.x = 0;
            }

            if (Input.GetAxisRaw("Vertical") > deadZone)
            {
                _movementDirection.y = 1;
            }
            else if (Input.GetAxisRaw("Vertical") < -(deadZone))
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
            if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                OnActivateButton2?.Invoke();
            }
        }
    }
}
