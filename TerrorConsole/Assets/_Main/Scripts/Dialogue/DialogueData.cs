using UnityEngine;

namespace TerrorConsole
{
    [System.Serializable]
    public class DialogueData 
    {
        [SerializeField][TextArea(3, 10)]
        private string[] _sentences;

        public string[] Sentences => _sentences;
    }
}
