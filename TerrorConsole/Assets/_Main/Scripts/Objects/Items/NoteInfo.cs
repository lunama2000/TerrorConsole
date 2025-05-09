using UnityEngine;

namespace TerrorConsole
{
    [CreateAssetMenu(fileName = "NewNoteInfo", menuName = "ScriptableObjects/NoteInfo")]

    public class NoteInfo : ItemInfo
    {
        public Sprite NoteBackground;
        public Sprite NoteDraw;
        [Multiline]
        public string NoteText;

        public override void OnClickedInInventory()
        {
            NotesUIManager.Source.DisplayNote(this);
        }
    }
}
