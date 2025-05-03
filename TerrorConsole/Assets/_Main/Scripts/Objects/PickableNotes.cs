using UnityEngine;

namespace TerrorConsole
{
    public class PickableNotes : PickableObject
    {
        protected override void PickedByPlayer()
        {
            NotesUIController.Source.DisplayNote((NoteInfo)_itemInfo);
            base.PickedByPlayer();
        }
    }
}
