using UnityEngine;

namespace TerrorConsole
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private DialogueData _dialogueData;
        [SerializeField] private LevelEventsRecorder _eventRecorder;

        private void Start()
        {
            gameObject.SetActive(!_eventRecorder.CheckEventState());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                DialogueManager.Source.StartDialogue(_dialogueData);
                _eventRecorder.RegisterLevelEvent(true);
                gameObject.SetActive(false);
            }
        }

        public void SwitchOn()
        {
            DialogueManager.Source.StartDialogue(_dialogueData);
        }
    }
}
