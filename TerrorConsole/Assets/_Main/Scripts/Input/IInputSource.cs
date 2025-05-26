using System;
using UnityEngine;

namespace TerrorConsole
{
    public interface IInputSource
    {
        public KeyCode GetActionInGameKeyCode(InputActionsInGame actionsInGame);
        event Action<InputType> OnInputTypeChange;
        Action OnActivateButton1 { get; set; }
        Action OnActivateButton2 { get; set; }
        Action OnActivateButton3 { get; set; }
        Action OnPauseButton { get; set; }
        Action OnInventoryButton { get; set; }
        bool IsMoving { get; }
        Vector2 MovementDirection { get; }
        Vector2 LastLookDirection{ get; }

        void SuscribeToInputActionsInGame(InputActionsInGame inputAction, Action callbackFunction);
        void UnSuscribeToInputActionsInGame(InputActionsInGame inputAction, Action callbackFunction);
    }
}