using UnityEngine;

namespace TerrorConsole
{
    [System.Serializable]
    public class DialogueData 
    {
        [TextArea(3, 10)]
        public string[] sentences;
    }
}
