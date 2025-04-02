using UnityEngine;

namespace TerrorConsole
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _continueButton;
        private ISaveSystemSource _saveManager;

        private void Start()
        {
            _saveManager = SaveSystemManager.Source;
            _continueButton.SetActive(_saveManager.GetLastLoadedFileIndex() != -1);
        }

        public void OnContinueButtonPressed()
        {
            OnGameFileButonPressed(_saveManager.GetLastLoadedFileIndex());
        }

        public void OnGameFileButonPressed(int fileIndex)
        {
            SaveGameData gameData = _saveManager.LoadGame(fileIndex);
            print(gameData);
            print(gameData.GetGameIndex());
            print(gameData.GetCurrentScene());
            ScreenTransitionManager.Source.TransitionToScene(gameData.GetCurrentScene());
        }
    }
}
