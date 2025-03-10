using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class DialogueManager : Singleton<IDialogueSource>, IDialogueSource
    {
        [Header("Components")]
        [SerializeField] private CanvasGroup _dialogueCanvas; 
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private Button _continueButton;
        
        [Header("Configuration")]
        [SerializeField] private float _dialogueAnimationDuration = 0.5f;
        [SerializeField] private float _dialogueTextSpeed = 6f;
        
        private readonly Queue<string> _sentences = new Queue<string>();

        private void Start()
        {
            _continueButton.onClick.AddListener(NextSentence);
        }

        public void StartDialogue(DialogueData dialogueData)
        {
            _dialogueCanvas.alpha = 0;
            _dialogueCanvas.gameObject.SetActive(true);
            _dialogueCanvas.DOFade(1f, _dialogueAnimationDuration);
            _continueButton.gameObject.SetActive(true);
            
            _sentences.Clear();
            foreach (string sentence in dialogueData.Sentences)
            {
                _sentences.Enqueue(sentence);
            }
            NextSentence();
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
            DOTween
                .To(() => text, x => text = x, sentence, sentence.Length / _dialogueTextSpeed)
                .OnUpdate(() => _dialogueText.text = text);
        }
        
        private void EndDialogue()
        {
            _dialogueCanvas
                .DOFade(0f, _dialogueAnimationDuration)
                .OnComplete(() =>
                {
                    _dialogueCanvas.gameObject.SetActive(false);
                    _continueButton.gameObject.SetActive(false);
                });
        }
    }
}
