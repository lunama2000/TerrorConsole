using System;
using System.Collections.Generic;
using UnityEngine;

namespace TerrorConsole
{
    public partial class SaveSystemManager : Singleton<ISaveSystemSource>, ISaveSystemSource
    {
        private SaveGameData _loadedGame;
        private int _lastLoadedGameFile = -1;
        private SaveConfigurationData _loadedConfiguration;
        
        [Header("CONFIGURATIONS")]
        [SerializeField] private bool _forceLoadGame = false;
        [SerializeField] private int _forceLoadGameIndex = 0;

        protected override void Awake()
        {
            base.Awake();
            
            if (MarkedForDestruction) return;
            
            UpdateLastLoadedGameFile(PlayerPrefs.GetInt("lastLoadedGameFile", -1));
            LoadConfigurations();

            if (_forceLoadGame)
            {
                LoadGame(_forceLoadGameIndex);
            }
        }

        private void Start()
        {
            ScreenTransitionManager.Source.OnTransitionBegan += SaveCurrentGame;
        }

        private void OnDestroy()
        {
            ScreenTransitionManager.Source.OnTransitionBegan -= SaveCurrentGame;
        }

        private void LoadConfigurations()
        {
            var configurationData = PlayerPrefs.GetString("gameConfiguration", null);
            if (string.IsNullOrEmpty(configurationData))
            {
                _loadedConfiguration = new SaveConfigurationData();
            }
            else
            {
                _loadedConfiguration = JsonUtility.FromJson<SaveConfigurationData>(configurationData);
            }
            
            LocalizationManager.Source.SetLanguage(_loadedConfiguration.LanguageCode);
        }

        private void SaveConfigurationData()
        {
            string json = JsonUtility.ToJson(_loadedConfiguration);
            PlayerPrefs.SetString("gameConfiguration", json);
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

        public ISaveGameData LoadGame(int fileIndex)
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

        ISaveGameData ISaveSystemSource.GetGameDataByIndex(int fileIndex)
        {
            return GetGameDataByIndex(fileIndex);
        }

        public int GetLastLoadedFileIndex()
        {
            return _lastLoadedGameFile;
        }

        public void SetInventory(List<ItemInfo> newInventory)
        {
            _loadedGame.SetInventory(newInventory);
            SaveCurrentGame();
        }

        public void SetCurrentScene(string currentScene)
        {
            _loadedGame.SetCurrentScene(currentScene);
            SaveCurrentGame();
        }
        
        public List<ItemInfo> GetInventory()
        {
            return _loadedGame.Inventory;
        }

        public void AddOrUpdateLevelEvent(int levelNumber, string eventName, bool eventState)
        {
            _loadedGame.GetLevelData(levelNumber).AddOrUpdateLevelEvent(eventName, eventState);
            SaveCurrentGame();
        }

        public bool GetEventState(int levelNumber, string eventName)
        {
            return _loadedGame.GetLevelData(levelNumber).GetEventState(eventName);
        }

        public void AddOrUpdateLevelParameter(int levelNumber, string parameterName, int parameterValue)
        {
            _loadedGame.GetLevelData(levelNumber).AddOrUpdateLevelParameter(parameterName, parameterValue);
            SaveCurrentGame();
        }

        public int GetLevelParameterValue(int levelNumber, string parameterName)
        {
            return _loadedGame.GetLevelData(levelNumber).GetParameterValue(parameterName);
        }

        public void SaveLanguageCode(string languageCode)
        {
            _loadedConfiguration.LanguageCode = languageCode;
            SaveConfigurationData();
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

        public void SaveSFXVolume(float newSFX)
        {
            _loadedConfiguration.SfxVolume = newSFX;
            SaveConfigurationData();
        }

        public void SaveMusicVolume(float newMusic)
        {
            _loadedConfiguration.MusicVolume = newMusic;
            SaveConfigurationData();
        }

        public float GetSavedMusicVolume()
        {
            return _loadedConfiguration.MusicVolume;
        }

        public float GetSavedSFXVolume()
        {
            return _loadedConfiguration.SfxVolume;
        }

        public void SaveResolution(int newIndex)
        {
            _loadedConfiguration.ResolutionIndex = newIndex;
            SaveConfigurationData();
        }

        public int GetSavedResolution()
        {
            return _loadedConfiguration.ResolutionIndex;
        }

        public string GetSavedLanguageCode()
        {
            return _loadedConfiguration.LanguageCode;
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
