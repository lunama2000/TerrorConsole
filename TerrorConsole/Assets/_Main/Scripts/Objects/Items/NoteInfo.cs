using UnityEngine;

namespace TerrorConsole
{
    [CreateAssetMenu(fileName = "NewNoteInfo", menuName = "ScriptableObjects/NoteInfo")]

    public class NoteInfo : ItemInfo
    {
        public Sprite NoteBackground;
        public Sprite NoteDraw;
        public string NoteText;

        public override void OnClickedInInventory()
        {
            NotesUIController.Source.DisplayNote(this);

        }
    }
}
