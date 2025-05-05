using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine.EventSystems;

namespace TerrorConsole
{
    public class NotesUIManager : Singleton<INotesUISource>, INotesUISource
    {
        [SerializeField] private GameObject _noteUI;
        [SerializeField] private CanvasGroup _noteUICanvasGroup;
        [SerializeField] private Image _noteBackgroud;
        [SerializeField] private Image _noteDraw;
        [SerializeField] private TextMeshProUGUI _noteTextIfDraw;
        [SerializeField] private TextMeshProUGUI _noteText;
        [SerializeField] private GameObject _okButton;
        [SerializeField] private UIController _uIController;

        public void CloseNote()
        {
            FadeOutNote().Forget();
        }

        private async UniTaskVoid FadeOutNote()
        {
            await _noteUICanvasGroup.DOFade(0, 0.5f).AsyncWaitForCompletion();
            _noteDraw.gameObject.SetActive(false);
            _noteText.gameObject.SetActive(false);
            _noteTextIfDraw.gameObject.SetActive(false);
            _noteUI.SetActive(false);
            _uIController.CloseMenu(_uIController);
            if (UIMenusManager.Source.GetOpenedMenusCount() == 0)
            {
                LevelManager.Source.PlayLevel();
            }
        }

        public void DisplayNote(NoteInfo noteToDisplay)
        {
            LevelManager.Source.PauseLevel();
            _noteBackgroud.sprite = noteToDisplay.NoteBackground;
            if (noteToDisplay.NoteDraw)
            {
                _noteDraw.sprite = noteToDisplay.NoteDraw;
                _noteTextIfDraw.text = noteToDisplay.NoteText;
                _noteDraw.gameObject.SetActive(true);
                _noteTextIfDraw.gameObject.SetActive(true);
            }
            else
            {
                _noteText.text = noteToDisplay.NoteText;
                _noteText.gameObject.SetActive(true);
            }

            _noteUICanvasGroup.alpha = 0;
            _noteUI.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_okButton);
            _noteUICanvasGroup.DOFade(1, 0.5f);
            _uIController.OpenMenu(_uIController);
        }
    }
}
