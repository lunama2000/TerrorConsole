using UnityEngine;
using UnityEngine.EventSystems;

namespace TerrorConsole
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseUI;
        [SerializeField] private GameObject _defaultSelected;

        private void Start()
        {
            InputManager.Source.OnPauseButton += OnInventoryButton;
        }

        private void OnDestroy()
        {
            InputManager.Source.OnPauseButton -= OnInventoryButton;
        }

        private void OnInventoryButton()
        {
            _pauseUI.SetActive(!_pauseUI.activeSelf);
            if (_pauseUI.activeSelf)
            {
                LevelManager.Source.PauseLevel();
                EventSystem.current.SetSelectedGameObject(_defaultSelected);
            }
            else
            {
                LevelManager.Source.PlayLevel();
            }
        }
    }
}
