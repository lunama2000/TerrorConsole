using System;
using UnityEngine;

namespace TerrorConsole
{
    public interface IInputSource
    {
        Action OnActivateButton1 { get; set; }
        Action OnActivateButton2 { get; set; }
        bool IsMoving { get; }
        Vector2 MovementDirection { get; }
        Vector2 LastLookDirection{ get; }
    }
}