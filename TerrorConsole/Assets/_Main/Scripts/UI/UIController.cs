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
        [SerializeField] private GameObject[] _menuUIButtons;
        [SerializeField] private GameObject[] _optionsUIButtons;
        [SerializeField] private GameObject[] _newGameUIButtons;

        public void MenuUIButtons()
            {
                EventSystem.current.SetSelectedGameObject(_menuUIButtons[1]);
            }

        public void OptionsUIButtons()
            {
                EventSystem.current.SetSelectedGameObject(_optionsUIButtons[0]);
            }
        public void NewGameUIButtons()
            {
                EventSystem.current.SetSelectedGameObject(_newGameUIButtons[0]);
            }

        public void AllMenusClosed()
            {
                EventSystem.current.SetSelectedGameObject(null);
            }

        public void ButtonLoadScene(string sceneName)
            {
                ScreenTransitionManager.Source.TransitionToScene(sceneName);
            }

            public void ButtonPauseLevel()
            {
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
        }
}
