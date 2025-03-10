using UnityEngine;

namespace TerrorConsole
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private DialogueData _dialogueData;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                DialogueManager.Source.StartDialogue(_dialogueData);
            }
        }
    }
}
