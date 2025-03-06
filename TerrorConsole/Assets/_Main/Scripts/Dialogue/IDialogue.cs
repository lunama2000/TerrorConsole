using UnityEngine;

namespace TerrorConsole
{
    public interface IDialogue
    {
        string ConversationName { get; }
        string[] Sentences { get; }
    }
}
