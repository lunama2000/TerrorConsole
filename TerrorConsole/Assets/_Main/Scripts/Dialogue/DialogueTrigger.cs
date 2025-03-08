using UnityEngine;

namespace TerrorConsole
{
    public class DialogueTrigger : MonoBehaviour
    {
        public DialogueData dialogueData;

        public void TriggerDialogue()
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogueData);
        }
    }
}
