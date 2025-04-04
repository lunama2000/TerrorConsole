using System;
using UnityEditor;
using UnityEngine;

namespace TerrorConsole
{
    public partial class SaveSystemManager : Singleton<ISaveSystemSource>, ISaveSystemSource
    {
        private SaveGameData _loadedGame;
        private int _lastLoadedGameFile = -1;
        private SaveConfigurationData _loadedConfiguration;

        override protected void Awake()
        {
            base.Awake();
            UpdateLastLoadedGameFile(PlayerPrefs.GetInt("lastLoadedGameFile", -1));
            LoadConfigurations();
        }

        private void LoadConfigurations()
        {
            string configurationData = PlayerPrefs.GetString("gameConfiguration", "ERROR");
            if (string.IsNullOrEmpty(configurationData) || configurationData == "ERROR")
            {
                _loadedConfiguration = new SaveConfigurationData();
            }
            else
            {
                _loadedConfiguration = JsonUtility.FromJson<SaveConfigurationData>(configurationData);
            }
        }

        public SaveConfigurationData GetConfigurationData()
        {
            return _loadedConfiguration;
        }

        public void DeleteConfigurationData()
        {
            PlayerPrefs.DeleteKey("gameConfiguration");
        }

        public void DeleteGame(int fileIndex)
        {
            PlayerPrefs.DeleteKey($"GameData{fileIndex}");
            UpdateLastLoadedGameFile(-1);
        }

        private void UpdateLastLoadedGameFile(int newLastLoadedGameFile)
        {
            _lastLoadedGameFile = newLastLoadedGameFile;
            PlayerPrefs.SetInt("lastLoadedGameFile", _lastLoadedGameFile);
        }

        public SaveGameData LoadGame(int fileIndex)
        {
            _loadedGame = GetGameDataByIndex(fileIndex);
            if (_loadedGame == null)
            {
                print($"Loading empty game file");
                _loadedGame = new SaveGameData(fileIndex);

                SaveCurrentGame();
            }
            else
            {
                print($"Game file loaded sucessfully");
            }
            UpdateLastLoadedGameFile(fileIndex);
            return _loadedGame;
        }

        public void SaveCurrentGame()
        {
            SaveGame(_loadedGame.GetGameIndex(), _loadedGame);
        }

        public void SaveGame(int fileIndex, SaveGameData data)
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString($"GameData{fileIndex}", json);
        }

        public bool CheckIfFileExist(int fileIndex)
        {
            string gameData = PlayerPrefs.GetString($"GameData{fileIndex}", "ERROR");
            if (gameData != "ERROR")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetLastLoadedFileIndex()
        {
            return _lastLoadedGameFile;
        }

        public SaveGameData GetLoadedGame()
        {
            return _loadedGame;
        }

        public SaveGameData GetGameDataByIndex(int fileIndex)
        {
            string gameDataString = PlayerPrefs.GetString($"GameData{fileIndex}", "ERROR");
            SaveGameData saveGameData;
            if (gameDataString != "ERROR")
            {
                try
                {
                    saveGameData = JsonUtility.FromJson<SaveGameData>(gameDataString);
                    return saveGameData;
                }
                catch (Exception e)
                {
                    print("Error loading the game file: " + e.Message);
                    return null;
                }
            }
            else
            {
                print($"Couldn't find game file with index: {fileIndex}");
                return null;
            }
        }
    }

#if UNITY_EDITOR
    public partial class SaveSystemManager
    {
        [ContextMenu("DeleteAllSaved")]
        public void DEBUG_DeleteAllSaved()
        {
            PlayerPrefs.DeleteAll();
        }
    }
#endif
}
