using System;
using UnityEngine;

namespace TerrorConsole
{
    public class SaveSystemManager : Singleton<ISaveSystemSource>, ISaveSystemSource
    {
        SaveGameData _loadedGame;
        public void DeleteGame(int fileIndex)
        {
            PlayerPrefs.DeleteKey($"GameData{fileIndex}");
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

}
