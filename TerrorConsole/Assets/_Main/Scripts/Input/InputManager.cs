using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TerrorConsole
{
    public enum InputType { None, KeyboardMouse, Gamepad }
    public enum InputActionsInGame { Button1, Button2, Button3, PauseButton, InventoryButton, LeftAxis, UISelect }
    
    [System.Serializable]
    public struct ActionInGameKeyCode
    {
        public InputActionsInGame action;
        public KeyCode inputKeyCode;
    }

    public class InputManager : Singleton<IInputSource>, IInputSource
    {
        [SerializeField] private InputType currentInputType = InputType.None;
        public event Action<InputType> OnInputTypeChange;

        private List<ActionInGameKeyCode> _currentActionsInGameKeyCodes = new List<ActionInGameKeyCode>();
        [SerializeField] private List<ActionInGameKeyCode> _GamepadActionsInGameKeyCodes = new List<ActionInGameKeyCode>();
        [SerializeField] private List<ActionInGameKeyCode> _KeyboardActionsInGameKeyCodes = new List<ActionInGameKeyCode>();


        [SerializeField] private float deadZone = 0.5f;

        public bool IsMoving { get; private set; }
        public Vector2 MovementDirection => _movementDirection.normalized;
        public Vector2 LastLookDirection => _lastLookDirection.normalized;

        public Action OnActivateButton1 { get; set; }
        public Action OnActivateButton2 { get; set; }
        public Action OnActivateButton3 { get; set; }
        public Action OnPauseButton { get; set; }
        public Action OnInventoryButton { get; set; }

        private Vector2 _movementDirection = Vector2.zero;
        private Vector2 _lastLookDirection = Vector2.zero;

        private bool _processInput = true;

        private void Start()
        {
            LevelManager.Source.OnLevelStateChange += OnLevelStateChange;
        }

        private void OnDestroy()
        {
            LevelManager.Source.OnLevelStateChange -= OnLevelStateChange;
        }

        private void OnLevelStateChange(LevelState newState)
        {
            switch (newState)
            {
                case LevelState.Play:
                case LevelState.InDialogue:
                case LevelState.Hiding:
                    _processInput = true;
                    break;

                default:
                    _processInput = false;
                    break;
            }
        }

        private void Update()
        {
            SetInputType();
            SetUIButtons();

            if (!_processInput)
                return;

            SetMovementInput();
            SetActionButtons();
            SetLookDirection();
        }

        private void SetInputType()
        {
            if (Gamepad.current != null && (Gamepad.current?.lastUpdateTime ?? 0) > (Keyboard.current?.lastUpdateTime ?? 0))
            {
                if (currentInputType != InputType.Gamepad)
                {
                    currentInputType = InputType.Gamepad;
                    UpdateCurrentInputType();
                }
            }
            else if ((Keyboard.current != null && (Keyboard.current?.lastUpdateTime ?? 0) > (Gamepad.current?.lastUpdateTime ?? 0)))
            {
                if (currentInputType != InputType.KeyboardMouse)
                {
                    currentInputType = InputType.KeyboardMouse;
                    UpdateCurrentInputType();
                }
            }
        }

        private void UpdateCurrentInputType()
        {
            switch (currentInputType)
            {
                case InputType.Gamepad: _currentActionsInGameKeyCodes = _GamepadActionsInGameKeyCodes; break;
                case InputType.KeyboardMouse: _currentActionsInGameKeyCodes = _KeyboardActionsInGameKeyCodes; break;
            }
            OnInputTypeChange?.Invoke(currentInputType);
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
            if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.JoystickButton1))    
            {
                OnActivateButton3?.Invoke();
            }
        }

        private void SetUIButtons()
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                OnPauseButton?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                OnInventoryButton?.Invoke();
            }
        }

        public KeyCode GetActionInGameKeyCode(InputActionsInGame actionsInGame)
        {
            foreach(ActionInGameKeyCode inputAction in _currentActionsInGameKeyCodes)
            {
                if(inputAction.action == actionsInGame)
                {
                    return inputAction.inputKeyCode;
                }
            }
            return KeyCode.None;
        }
    }
}
