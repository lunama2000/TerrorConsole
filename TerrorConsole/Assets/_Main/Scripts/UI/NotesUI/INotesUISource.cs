using UnityEngine;

namespace TerrorConsole
{
    public interface INotesUISource
    {
        void DisplayNote(NoteInfo noteToDisplay);
        void CloseNote();
    }
}
