using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace TerrorConsole
{

    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject _defaultSelectedUI;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private bool _startOpened = false;

        private void Start()
        {
            if (_startOpened)
            {
                OpenMenu(this);
            }
        }

        public void OpenMenu(UIController newMenu)
        {
            UIMenusManager.Source.OpenNewMenuOnTop(newMenu);
        }

        public void CloseMenuOnTop()
        {
            UIMenusManager.Source.CloseMenuOnTop();
        }
        public void CloseMenu(UIController menuToClose)
        {
            UIMenusManager.Source.CloseMenu(menuToClose);
        }

        public GameObject GetDefaultSelectedUI()
        {
            return _defaultSelectedUI;
        }

        public CanvasGroup GetCanvasGroup()
        {
            return _canvasGroup;
        }

        public void AllMenusClosed()
        {
            SetSelectedGameObject(null);
        }

        private void SetSelectedGameObject(GameObject selectedGameObject)
        {
            EventSystem.current.SetSelectedGameObject(selectedGameObject);
            var handler = selectedGameObject.GetComponent<ISelectHandler>();
            if (handler != null)
            {
                var data = new BaseEventData(EventSystem.current);
                handler.OnSelect(data);
            }
        }

        public void ButtonLoadScene(string sceneName)
        {
            ScreenTransitionManager.Source.TransitionToScene(sceneName);
        }

        public void ButtonPauseLevel()
        {
            SetSelectedGameObject(_defaultSelectedUI);
            LevelManager.Source.PauseLevel();
        }
        
        private void ButtonResumeLevel()
        {
            LevelManager.Source.PlayLevel();
        }

        public void QuitApplication()
        {
            Application.Quit();
        }
        
        public void ButtonPlaySFX()
        {
            AudioManager.Source.PlaySFX("UIButtonClickSFX");
        }

        private void OnEnable()
        {
            SetSelectedGameObject(_defaultSelectedUI);
        }
    }
}
