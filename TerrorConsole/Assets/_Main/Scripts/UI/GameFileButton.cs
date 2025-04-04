using TMPro;
using UnityEngine;

namespace TerrorConsole
{
    public class GameFileButton : MonoBehaviour
    {
        [SerializeField] private MainMenuController mainMenuController;
        [SerializeField] private int _gameFileIndex;
        [SerializeField] private GameObject _newGameLabel;
        [SerializeField] private GameObject _gameSaveInfoUI;
        [SerializeField] private TextMeshProUGUI _fileNumberTxt;
        [SerializeField] private TextMeshProUGUI _stageTxt;
        [SerializeField] private GameObject _lanternIcon;
        SaveGameData _saveGameData;

        private void Start()
        {
            LoadFileUIInfo();
        }

        private void LoadFileUIInfo()
        {
            _saveGameData = SaveSystemManager.Source.GetGameDataByIndex(_gameFileIndex);
            if (_saveGameData == null)
            {
                _newGameLabel.SetActive(true);
                _gameSaveInfoUI.SetActive(false);
            }
            else
            {
                _newGameLabel.SetActive(false);
                _gameSaveInfoUI.SetActive(true);
                _fileNumberTxt.text = _gameFileIndex.ToString();
                _stageTxt.text = _saveGameData.GetCurrentScene();
                _lanternIcon.SetActive(_saveGameData.GetInventory().Contains("ItemLinterna"));
            }
        }

        public void OnDeleteGameFileButonPressed()
        {
            SaveSystemManager.Source.DeleteGame(_gameFileIndex);
            LoadFileUIInfo();
            mainMenuController.SetupContinueButton();
        }
    }
}
