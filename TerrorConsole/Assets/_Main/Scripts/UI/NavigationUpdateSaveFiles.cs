using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class NavigationUpdateSaveFiles : MonoBehaviour
    {
        [Header("Referencias de UI")]
        [SerializeField] private List<Button> _saveFileButtons; 
        [SerializeField] private List<Button> _deleteButtons;
        [SerializeField] private Button _backButton;

        private void Start()
        {
            UpdateNavigation();
        }

        public void UpdateNavigation()
        {
            for (int i = 0; i < _saveFileButtons.Count; i++)
            {
                var nav = new Navigation { mode = Navigation.Mode.Explicit };
                
                if (_deleteButtons[i].gameObject.activeInHierarchy && _deleteButtons[i].interactable)
                {
                    nav.selectOnDown = _deleteButtons[i];
                }
                else
                {
                    nav.selectOnDown = _backButton;
                }
                
                nav.selectOnLeft = _saveFileButtons[(i - 1 + _saveFileButtons.Count) % _saveFileButtons.Count];
                nav.selectOnRight = _saveFileButtons[(i + 1) % _saveFileButtons.Count];
                _saveFileButtons[i].navigation = nav;
            }
            
            for (int i = 0; i < _deleteButtons.Count; i++)
            {
                if (!_deleteButtons[i].gameObject.activeInHierarchy) continue;

                var nav = new Navigation { mode = Navigation.Mode.Explicit };
                nav.selectOnUp = _saveFileButtons[i];
                nav.selectOnDown = _backButton;
                _deleteButtons[i].navigation = nav;
            }
            
            for (int i = _deleteButtons.Count - 1; i >= 0; i--)
            {
                if (_deleteButtons[i].gameObject.activeInHierarchy)
                {
                    var nav = new Navigation { mode = Navigation.Mode.Explicit };
                    nav.selectOnUp = _deleteButtons[i];
                    _backButton.navigation = nav;
                    break;
                }
            }
            
        }
        
        public void RefreshBackNavigation()
        {
            var backNav = new Navigation { mode = Navigation.Mode.Explicit };
            
            for (int i = _deleteButtons.Count - 1; i >= 0; i--)
            {
                if (_deleteButtons[i].gameObject.activeInHierarchy && _deleteButtons[i].interactable)
                {
                    backNav.selectOnUp = _deleteButtons[i];
                    _backButton.navigation = backNav;
                    return;
                }
            }
            
            for (int i = _saveFileButtons.Count - 1; i >= 0; i--)
            {
                if (_saveFileButtons[i].gameObject.activeInHierarchy && _saveFileButtons[i].interactable)
                {
                    backNav.selectOnUp = _saveFileButtons[i];
                    _backButton.navigation = backNav;
                    return;
                }
            }
            
            _backButton.navigation = backNav;
        }
    }
}
