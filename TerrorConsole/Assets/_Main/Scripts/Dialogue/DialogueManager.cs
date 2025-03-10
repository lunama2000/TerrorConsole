using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class DialogueManager : MonoBehaviour
    {
        
        [FormerlySerializedAs("_dialogueCanvas")]
        [Header("Components")]
        [SerializeField] private CanvasGroup dialogueCanvas; 
        [SerializeField] private TextMeshProUGUI dialogueText;
        
        [Header("Configuration")]
        [SerializeField] private float dialogueDuration;
        [SerializeField] private float dialogueAnimationDuration = 0.5f;
        [SerializeField] private float dialogueTextSpeed = 6f;
        
        private Queue<string> _sentences;

        void Start() 
        {
            _sentences = new Queue<string>();
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
            dialogueCanvas.alpha = 0;
            dialogueCanvas.gameObject.SetActive(true);
            dialogueCanvas.DOFade(1f, dialogueAnimationDuration);//.OnComplete(() =>
            {
                _sentences.Clear();
                foreach (string sentence in dialogueData.sentences)
                {
                    _sentences.Enqueue(sentence);
                }
                NextSentence();
            }//);
        }
        
        
        public void NextSentence()
        {
            if (_sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            
            string sentence = _sentences.Dequeue();
            AnimateText(sentence);
        }

        private void AnimateText(string sentence)
        {
            string text = "";
            DOTween.To(() => text, x => text = x, sentence, sentence.Length / dialogueTextSpeed).OnUpdate(() =>
            {
                dialogueText.text = text;
            });
            
        }
        
        void EndDialogue()
        {
            dialogueCanvas.DOFade(0f, dialogueAnimationDuration)
                .OnComplete(() => dialogueCanvas.gameObject.SetActive(false));
        }
        
        public enum DialogueTransitionType
        {
            Open,
            Close
        }
    }
}
