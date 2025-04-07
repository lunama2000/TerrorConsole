using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private RectTransform _selector;
        [SerializeField] private RectTransform[] _referenceObject;
        [SerializeField] private Transform[] _optionsItems;
        [SerializeField] private Transform[] _menuItems;
        private Transform[] _currentArrayInUse;
        //private Image[] optionsImages;
        int _currentSelection = 0;

        bool _allowedToSellect = false;

        CUREENT_MENU_STATE _currentState = CUREENT_MENU_STATE.MAIN_MENU;
        [SerializeField] GameObject _optionsScreen;

        enum CUREENT_MENU_STATE
        {
            MAIN_MENU,
            OPTIONS
        }

        void Start()
        {
            _currentArrayInUse = _menuItems;
            _allowedToSellect = false;
            _currentSelection = 0;
            ChangeSelectorPosition();
        }
        void ChangeSelectorPosition()
        { 
            _selector.SetParent(_currentArrayInUse[_currentSelection]);
            if (_currentState == CUREENT_MENU_STATE.MAIN_MENU)
            {
                _selector.anchoredPosition = new Vector2(-70, 8);
                _selector.sizeDelta = new Vector2(30,30);
            }
            else if (_currentState == CUREENT_MENU_STATE.OPTIONS)
            {
                _selector.anchoredPosition = new Vector2(0, 8);
                _selector.sizeDelta = new Vector2(30, 30);
            }

        }

        void ChangeCurrentSelection(bool _add = true)
        {

            if (_add)
            {
                _currentSelection++;
                if (_currentSelection >= _currentArrayInUse.Length)
                {
                    _currentSelection = 0;
                }
            }
            else
            {
                _currentSelection--;
                if (_currentSelection < 0)
                {
                    _currentSelection = _currentArrayInUse.Length - 1;
                }
            }
        }

        void ChangedCurrentSelectionOptionIsFound(bool _add = true)
        {
            ChangeCurrentSelection(_add);

            while (!_currentArrayInUse[_currentSelection].gameObject.activeInHierarchy)
            {
                ChangeCurrentSelection(_add);
            }
            ChangeSelectorPosition();
        }

        void Update()
        {
            switch (_currentState)
            {
                case CUREENT_MENU_STATE.MAIN_MENU:
                    if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        ChangedCurrentSelectionOptionIsFound();
                    }
                    else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        ChangedCurrentSelectionOptionIsFound(false);
                    }
                    else if (Input.GetKeyDown(KeyCode.Space))
                    {
                        if (_currentSelection == 4)
                        {
                            Button boton = _currentArrayInUse[_currentSelection].GetComponent<Button>();
                            _currentState = CUREENT_MENU_STATE.OPTIONS;
                            _currentSelection = 0;
                            _currentArrayInUse = _optionsItems;
                            ChangeSelectorPosition();

                            if (boton != null)
                            {
                                boton.onClick.Invoke();
                            }
                        }
                        /*else if (currentSelection == 1)
                        {
                            
                        }*/
                    }
                    break;
                }
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
