using UnityEngine;

namespace TerrorConsole
{
    public interface IInputSource
    {
        bool IsMoving { get; }
        Vector2 MovementDirection { get; }
    }
}