using UnityEngine;
using UnityEngine.Events;

namespace TerrorConsole
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private DialogueData _dialogueData;
        [SerializeField] private LevelEventsRecorder _eventRecorder;
        [SerializeField] private bool _disbleIfRead = true;
        [SerializeField] private UnityEvent _onTriggerEnter = new UnityEvent();

        private void Start()
        {
            if(_disbleIfRead)
                gameObject.SetActive(!_eventRecorder.CheckEventState());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                DialogueManager.Source.StartDialogue(_dialogueData);
                _eventRecorder.RegisterLevelEvent(true);
                gameObject.SetActive(false);
                _onTriggerEnter?.Invoke();
            }
        }

        public void SwitchOn()
        {
            DialogueManager.Source.StartDialogue(_dialogueData);
        }
    }
}
