using UnityEngine;

namespace TerrorConsole
{
    public interface IInputSource
    {
        bool ActionButton1 { get; }
        bool IsMoving { get; }
        Vector2 MovementDirection { get; }
    }
}