using System;
using System.Collections.Generic;
using UnityEngine;

namespace TerrorConsole
{
    [Serializable]
    public class SaveGameData
    {
        [SerializeField] private int _gameIndex;
        [SerializeField] private string _currentScene;
        [SerializeField] private List<string> _inventory;

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

        public string GetCurrentScene()
        {
            return _currentScene;
        }

        public void SetCurrentScene(string sceneName)
        {
            _currentScene = sceneName;
        }

        public List<string> GetInventory()
        {
            return _inventory;
        }

        public void SetInventory(List<string> newInventory)
        {
            _inventory = newInventory;
        }
    }
}
