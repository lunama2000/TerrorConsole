using UnityEngine;
using UnityEngine.EventSystems;

namespace TerrorConsole
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private UIController _pauseUI;
        private bool _paused;
        private LevelState _lastLevelState;

        private void Start()
        {
            _lastLevelState = LevelState.Play;
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
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        public void ResumeGame()
        {
            UIMenusManager.Source.CloseMenuOnTop();
            LevelManager.Source.ChangeLevelState(_lastLevelState);
        }

        public void PauseGame()
        {
            _lastLevelState = LevelManager.Source.GetCurrentLevelState();
            LevelManager.Source.PauseLevel();
            UIMenusManager.Source.OpenNewMenuOnTop(_pauseUI);
        }

        public void GoToMainMenu(string mainMenuSceneName)
        {
            UIMenusManager.Source.CloseAllMenus();
            ScreenTransitionManager.Source.TransitionToScene(mainMenuSceneName);
        }
    }
}
