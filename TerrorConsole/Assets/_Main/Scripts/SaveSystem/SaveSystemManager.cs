using System;
using UnityEngine;

namespace TerrorConsole
{
    public class SaveSystemManager : Singleton<ISaveSystemSource>, ISaveSystemSource
    {
        private SaveGameData _loadedGame;
        private int _lastLoadedGameFile = -1;

        private void Start()
        {
            _lastLoadedGameFile = PlayerPrefs.GetInt("lastLoadedGameFile", -1);
        }
        public void DeleteGame(int fileIndex)
        {
            PlayerPrefs.DeleteKey($"GameData{fileIndex}");
            UpdateLastLoadedGameFile(-1);
        }

        private void UpdateLastLoadedGameFile(int newLastLoadedGameFile)
        {
            _lastLoadedGameFile = newLastLoadedGameFile;
            PlayerPrefs.GetInt("lastLoadedGameFile", _lastLoadedGameFile);
        }

        public void LoadGame(int fileIndex)
        {
            string gameData = PlayerPrefs.GetString($"GameData{fileIndex}", "ERROR");
            if (gameData != "Error")
            {
                try
                {
                    _loadedGame = JsonUtility.FromJson<SaveGameData>(gameData);
                }
                catch (Exception e)
                {
                    print("No se pudo cargar la partida: " + e.Message);
                }
            }
            else
            {
                print($"No se encontró una partida con el index {fileIndex}. Cargando una vacia");
                _loadedGame = new SaveGameData(fileIndex);
            }
            UpdateLastLoadedGameFile(fileIndex);
        }

        public void SaveCurrentGame()
        {
            SaveGame(_loadedGame._gameIndex);
        }

        public void SaveProgress(int currentLevel, string inventory)
        {
            _loadedGame._currentLevel = currentLevel;
        }

        public void SaveGame(int fileIndex)
        {
            string json = JsonUtility.ToJson(_loadedGame);
            PlayerPrefs.SetString($"GameData{fileIndex}", json);
        }

        public bool CheckIfFileExist(int fileIndex)
        {
            string gameData = PlayerPrefs.GetString($"GameData{fileIndex}", "ERROR");
            if (gameData != "Error")
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
    }

    [Serializable]
    public class SaveGameData
    {
        public int _gameIndex;
        public int _currentLevel;

        public SaveGameData(int levelIndex)
        {
            _gameIndex = levelIndex;
        }
    }

    [Serializable]
    public class SaveConfigurationData
    {
        public int _sfxVolume;
        public int _musicVolume;
        public int _lenguageIndex;

        public SaveConfigurationData(int sfxVolume, int musicVolume, int lenguageIndex)
        {
            _sfxVolume = sfxVolume;
            _musicVolume = musicVolume;
            _lenguageIndex = lenguageIndex;
        }
    }

}
