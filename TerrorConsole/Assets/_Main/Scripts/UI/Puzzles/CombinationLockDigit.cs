using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class CombinationLockDigit : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private int _digitIndex;
        [SerializeField] private Image _selectedMarker;
        private bool _isSelected;

        private List<char> _digitsOptions = new List<char>();
        [SerializeField] private List<Sprite> _spriteOptions = new List<Sprite>();

        private int _currentDigitIndex;
        [SerializeField] private Image _centerDigit;
        private Vector3 _centerDigitPos;
        [SerializeField] private Image _upDigit;
        private Vector3 _upDigitPos;
        [SerializeField] private Image _downDigit;
        private Vector3 _downDigitPos;

        private Tween _currentTransitionCenterDigit;
        private Tween _currentTransitionComplementaryDigit;

        private bool _isTransitionHappening;

        [SerializeField] private string _sfxClickKey;


        private void Start()
        {
            for (char c = 'A'; c <= 'Z'; c++)
            {
                _digitsOptions.Add(c);
            }
            CombinationLockPuzzle.Source.UpdateDigit(_digitIndex, _digitsOptions[_currentDigitIndex]);
            _centerDigitPos = _centerDigit.transform.localPosition;
            _upDigitPos = _upDigit.transform.localPosition;
            _downDigitPos = _downDigit.transform.localPosition;
            if (_digitsOptions.Count != _spriteOptions.Count)
            {
                Debug.LogError("The count of the digit lists doesn't match the cout of sprites, please make sure each digit has its own sprite in the list");
            }
            ResetDigitsPos();
        }

        public void OnSelect(BaseEventData eventData)
        {
            _selectedMarker.gameObject.SetActive(true);
            _isSelected = true;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            _isSelected = false;
            _selectedMarker.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            InputManager.Source.OnActivateUp += OnUpPressed;
            InputManager.Source.OnActivateDown += OnDownPressed;
        }

        private void OnDisable()
        {
            InputManager.Source.OnActivateUp -= OnUpPressed;
            InputManager.Source.OnActivateDown -= OnDownPressed;
        }

        private void OnUpPressed()
        {
            if (_isSelected)
            {
                DoTransitionCenterDigit(_centerDigit.rectTransform, _upDigitPos, 1).Forget();
                DoTransitionComplementaryDigit(_downDigit.rectTransform, _centerDigitPos).Forget();
                PlayClickSFX();
            }
        }

        private void PlayClickSFX()
        {
            if (!string.IsNullOrEmpty(_sfxClickKey))
            {
                AudioManager.Source.PlaySFX(_sfxClickKey);
            }
        }

        private void OnDownPressed()
        {
            if (_isSelected)
            {
                DoTransitionCenterDigit(_centerDigit.rectTransform, _downDigitPos, -1).Forget();
                DoTransitionComplementaryDigit(_upDigit.rectTransform, _centerDigitPos).Forget();
                PlayClickSFX();
            }
        }

        private async UniTaskVoid DoTransitionCenterDigit(RectTransform digitToMove, Vector2 destinyPos, int indexModifier)
        {
            if (_isTransitionHappening && _currentTransitionCenterDigit != null && _currentTransitionCenterDigit.IsActive())
            {
                _currentTransitionCenterDigit.timeScale = 100f;
                await _currentTransitionCenterDigit.AsyncWaitForCompletion();
            }

            _isTransitionHappening = true;

            _currentTransitionCenterDigit = digitToMove.DOAnchorPos(destinyPos, 0.3f).SetEase(Ease.OutQuad);

            await _currentTransitionCenterDigit.AsyncWaitForCompletion();

            _currentDigitIndex = IncreaseOrDecreaseIndex(_currentDigitIndex, indexModifier);
            ResetDigitsPos();

            _isTransitionHappening = false;
        }

        private async UniTaskVoid DoTransitionComplementaryDigit(RectTransform digitToMove, Vector2 destinyPos)
        {
            if (_isTransitionHappening && _currentTransitionComplementaryDigit != null && _currentTransitionComplementaryDigit.IsActive())
            {
                _currentTransitionComplementaryDigit.timeScale = 100f;
                await _currentTransitionComplementaryDigit.AsyncWaitForCompletion();
            }

            _isTransitionHappening = true;

            _currentTransitionComplementaryDigit = digitToMove.DOAnchorPos(destinyPos, 0.3f).SetEase(Ease.OutQuad).OnComplete(ResetDigitsPos);
            _currentTransitionComplementaryDigit.timeScale = 1f;

            await _currentTransitionComplementaryDigit.AsyncWaitForCompletion();
            _isTransitionHappening = false;
        }


        private void ResetDigitsPos()
        {
            CombinationLockPuzzle.Source.UpdateDigit(_digitIndex, _digitsOptions[_currentDigitIndex]);
            _centerDigit.transform.localPosition = _centerDigitPos;
            _centerDigit.sprite = _spriteOptions[_currentDigitIndex];

            _upDigit.transform.localPosition = _upDigitPos;
            _upDigit.sprite = _spriteOptions[IncreaseOrDecreaseIndex(_currentDigitIndex,-1)];

            _downDigit.transform.localPosition = _downDigitPos;
            _downDigit.sprite = _spriteOptions[IncreaseOrDecreaseIndex(_currentDigitIndex, 1)];
        }

        private int IncreaseOrDecreaseIndex(int index, int modifier)
        {
            index += modifier;
            if (index < 0)
            {
                index = _spriteOptions.Count + index;
            }
            else if (index >= _spriteOptions.Count)
            {
                index = index - _spriteOptions.Count;
            }

            return index;
        }
    }
}
