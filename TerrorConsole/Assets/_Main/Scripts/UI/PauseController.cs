using UnityEngine;
using UnityEngine.EventSystems;

namespace TerrorConsole
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private UIController _pauseUI;
        private bool _paused;

        private void Start()
        {
            InputManager.Source.OnPauseButton += OnPauseButton;
        }

        private void OnDestroy()
        {
            InputManager.Source.OnPauseButton -= OnPauseButton;
        }

        private void OnPauseButton()
        {
            _paused = !_paused;
            if (_paused)
            {
                LevelManager.Source.PauseLevel();
                UIMenusManager.Source.OpenNewMenuOnTop(_pauseUI);
            }
            else
            {
                UIMenusManager.Source.CloseMenuOnTop();
                LevelManager.Source.PlayLevel();
            }
        }
    }
}
