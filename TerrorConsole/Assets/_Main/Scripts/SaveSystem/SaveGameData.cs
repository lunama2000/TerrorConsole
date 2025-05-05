using System;
using System.Collections.Generic;
using UnityEngine;

namespace TerrorConsole
{
    public interface ISaveGameData
    {
        string CurrentScene { get; }
        List<ItemInfo> Inventory { get; }
    }
    
    [Serializable]
    public class SaveGameData: ISaveGameData
    {
        [SerializeField] private int _gameIndex;
        [SerializeField] private string _currentScene;
        [SerializeField] private List<ItemInfo> _inventory;
        [SerializeField] private List<SaveLevelData> _levelsData;

        public string CurrentScene => _currentScene;
        public List<ItemInfo> Inventory => _inventory;
        
        public SaveGameData(int gameFileIndex)
        {
            SetGameIndex(gameFileIndex);
            SetCurrentScene("Tutorial");
        }

        public int GetGameIndex()
        {
            return _gameIndex;
        }

        public void SetGameIndex(int newIndex)
        {
            _gameIndex = newIndex;
        }

        public void SetCurrentScene(string sceneName)
        {
            _currentScene = sceneName;
        }

        public void SetInventory(List<ItemInfo> newInventory)
        {
            _inventory = newInventory;
        }

        public List<SaveLevelData> GetLevelsData()
        {
            if (_levelsData == null)
            {
                _levelsData = new List<SaveLevelData>();
            }

            return _levelsData;
        }

        public SaveLevelData GetLevelData(int levelNumber)
        {
            if (_levelsData == null)
            {
                _levelsData = new List<SaveLevelData>();
            }

            if (_levelsData.Count <= levelNumber)
            {
                _levelsData.Add(new SaveLevelData(levelNumber));
            }
            return _levelsData[levelNumber];
        }

        public void SetLevelsData(List<SaveLevelData> newLevelsData)
        {
            _levelsData = newLevelsData;
        }

        public void SetLevelData(SaveLevelData newLevelData, int levelIndex)
        {
            try
            {
                _levelsData[levelIndex] = newLevelData;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error saving the level data: {ex}");
            }
        }
    }
}
