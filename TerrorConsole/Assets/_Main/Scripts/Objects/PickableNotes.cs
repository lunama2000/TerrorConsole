using System;
using UnityEngine;

namespace TerrorConsole
{
    public class PickableNotes : PickableObject
    {
        protected override void PickedByPlayer()
        {
            NotesUIManager.Source.DisplayNote((NoteInfo)_itemInfo);
            base.PickedByPlayer();
        }
    }
}
