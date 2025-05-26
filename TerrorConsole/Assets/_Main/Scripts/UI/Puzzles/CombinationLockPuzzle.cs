using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace TerrorConsole
{
    public class CombinationLockPuzzle : Singleton<ICombinationLockPuzzleSource>, ICombinationLockPuzzleSource
    {
        [SerializeField] private UIController _myUIController;
        [SerializeField] private RectTransform _lockUI;
        [SerializeField] private List<char> _correctAnswer = new List<char>(3);
        private List<char> _currentValues = new List<char>(3);

        [SerializeField] private Door _doorToOpen;
        [SerializeField] private GameObject _interactuableLock;

        protected override void Awake()
        {
            base.Awake();
            foreach (char c in _correctAnswer)
            {
                _currentValues.Add('A');
            }
        }

        private void OnEnable()
        {
            LevelManager.Source.ChangeLevelState(LevelState.InDialogue);
            InputManager.Source.OnActivateButton1 += CloseCombinationLock;
            InputManager.Source.OnActivateButton2 += ValidateCombination;
            TooltipsManager.Source.StashCurrentUITooltips();
            TooltipsManager.Source.ShowUITooltip(InputActionsInGame.Button1, "Close");
            TooltipsManager.Source.ShowUITooltip(InputActionsInGame.Button2, "Validate");
        }

        private void OnDisable()
        {
            LevelManager.Source.ChangeLevelState(LevelState.Play);
            InputManager.Source.OnActivateButton1 -= CloseCombinationLock;
            InputManager.Source.OnActivateButton2 -= ValidateCombination;
            TooltipsManager.Source.HideUITooltip("Close");
            TooltipsManager.Source.HideUITooltip("Validate");
            TooltipsManager.Source.UnStashUITooltips();
        }

        private void CloseCombinationLock()
        {
            _myUIController.CloseMenu(_myUIController);
        }

        private void ValidateCombination()
        {
            for(int i = 0; i < _correctAnswer.Count; i++)
            {
                if(_correctAnswer[i] != _currentValues[i])
                {
                    ShakeX(_lockUI);
                    return;
                }
            }
            print("COMPLETED");
            OnCompleted();
        }

        public void UpdateDigit(int index, char value)
        {
            _currentValues[index] = value;
        }

        public void ShakeX(RectTransform target)
        {
            Vector3 originalPos = target.anchoredPosition;

            target.DOAnchorPosX(originalPos.x + 15f, 0.1f).SetEase(Ease.InOutQuad).SetLoops(4, LoopType.Yoyo).OnComplete(() => target.anchoredPosition = originalPos); 
        }

        void OnCompleted()
        {
            _doorToOpen.OpenDoor();
            CloseCombinationLock();
            _interactuableLock.gameObject.SetActive(false);
        }
    }
}
