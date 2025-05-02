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
        [SerializeField] private GameObject defaultSelectedUI;
        [SerializeField] private GameObject[] _menuUIButtons;
        [SerializeField] private GameObject[] _optionsUIButtons;
        [SerializeField] private GameObject[] _newGameUIButtons;

        public void MenuUIButtons()
        {
            SetSelectedGameObject(_menuUIButtons[1]);
        }

        public void OptionsUIButtons()
        {
            SetSelectedGameObject(_optionsUIButtons[0]);
        }

        public void NewGameUIButtons()
            {
                SetSelectedGameObject(_newGameUIButtons[0]);
            }

        public void AllMenusClosed()
        {
            SetSelectedGameObject(null);
        }

        private void SetSelectedGameObject(GameObject selectedGameObject)
        {
            EventSystem.current.SetSelectedGameObject(selectedGameObject);
        }

        public void ButtonLoadScene(string sceneName)
        {
            ScreenTransitionManager.Source.TransitionToScene(sceneName);
        }

        public void ButtonPauseLevel()
        {
            SetSelectedGameObject(defaultSelectedUI);
            LevelManager.Source.PauseLevel();
        }
        
        public void ButtonResumeLevel()
        {
            LevelManager.Source.PlayLevel();
        }
        
        public void ButtonPlaySFX()
        {
            AudioManager.Source.PlayUIButtonClickSFX();
        }

        private void OnEnable()
        {
            SetSelectedGameObject(defaultSelectedUI);
        }
    }
}
