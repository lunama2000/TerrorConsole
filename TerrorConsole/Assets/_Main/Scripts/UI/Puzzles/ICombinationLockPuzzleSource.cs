using UnityEngine;

namespace TerrorConsole
{
    public interface ICombinationLockPuzzleSource
    {
        void UpdateDigit(int index, char value);
    }
}
