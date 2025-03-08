using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class DialogueManager : MonoBehaviour
    {
        
        [FormerlySerializedAs("dialogueCanvas")]
        [Header("Components")]
        [SerializeField] private Canvas _dialogueCanvas; 
        [SerializeField] private GameObject dialogueBox; 
        [SerializeField] private TextMeshProUGUI dialogueText;
        
        [Header("Configuration")]
        [SerializeField] private float dialogueDuration;
        [SerializeField] private float dialogueAnimationDuration;
        
        private Queue<string> sentences;

        void Start() 
        {
            sentences = new Queue<string>();
        }
        
        /*public void Transition(Action action, DialogueTransitionType transitionType)
        {
            switch (transitionType)
            {
                case DialogueTransitionType.Open:
                    TransitionFade(action).Forget();
                    break;
                case DialogueTransitionType.Close:
                    TransitionSlide(action).Forget();
                    break;
            }
        }
        
        private TransitionFade(Action onTransition)
        {
            _dialogueCanvas.alpha = 0;
            _dialogueCanvas.gameObject.SetActive(true);
            await _dialogueCanvas.DOFade(1, _transitionDuration).AsyncWaitForCompletion();

            onTransition?.Invoke();

            await _dialogueCanvas.DOFade(0, _transitionDuration).AsyncWaitForCompletion();
            _dialogueCanvas.gameObject.SetActive(false);
        }*/
        public void StartDialogue(DialogueData dialogueData)
        {
            _dialogueCanvas.gameObject.SetActive(true);
            sentences.Clear();

            foreach (string sentence in dialogueData.sentences)
            {
                sentences.Enqueue(sentence);
            }
            NextSentence();
        }
        
        
        public void NextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            
            string sentence = sentences.Dequeue();
            dialogueText.text = sentence;
        }

        void EndDialogue()
        {
            dialogueBox.SetActive(false);
        }
        
        public enum DialogueTransitionType
        {
            Open,
            Close
        }
    }
}
