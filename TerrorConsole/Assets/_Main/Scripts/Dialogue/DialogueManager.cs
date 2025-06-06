using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

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

        private LevelState _lastLevelState;


        private readonly Queue<string> _sentences = new Queue<string>();

        private string _currentSentence;
        private bool _isTextTransitionHappenig;
        private Tween _currentTransition;

        private void Start()
        {
            _continueButton.onClick.AddListener(OnContinueClicked);
        }

        public void StartDialogue(DialogueData dialogueData)
        {
            _lastLevelState = LevelManager.Source.GetCurrentLevelState();
            LevelManager.Source.ChangeLevelState(LevelState.InDialogue);
            _dialogueCanvas.alpha = 0;
            _dialogueCanvas.gameObject.SetActive(true);
            _dialogueCanvas.DOFade(1f, _dialogueAnimationDuration);
            _continueButton.gameObject.SetActive(true);

            InputManager.Source.OnActivateButton1 += OnContinueClicked;


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

            _currentSentence = _sentences.Dequeue().Localize();
            AnimateText(_currentSentence).Forget();
        }

        private async UniTaskVoid AnimateText(string sentence)
        {
            _isTextTransitionHappenig = true;
            string text = "";
            _currentTransition = DOTween
                .To(() => text, x => text = x, sentence, sentence.Length / _dialogueTextSpeed)
                .OnUpdate(() => _dialogueText.text = text);
            await _currentTransition.AsyncWaitForCompletion();
            _isTextTransitionHappenig = false;
            _currentTransition = null;
        }
        
        private void EndDialogue()
        {
            LevelManager.Source.ChangeLevelState(_lastLevelState);
            _dialogueCanvas
                .DOFade(0f, _dialogueAnimationDuration)
                .OnComplete(() =>
                {
                    _dialogueCanvas.gameObject.SetActive(false);
                    _continueButton.gameObject.SetActive(false);
                });

            InputManager.Source.OnActivateButton1 -= OnContinueClicked;
        }

        private void OnContinueClicked()
        {
            if (_isTextTransitionHappenig)
            {
                _currentTransition.Kill();
                _dialogueText.text = _currentSentence;
                _isTextTransitionHappenig = false;
                _currentTransition = null;
            }
            else
            {
                NextSentence();
            }
        }
    }
}
