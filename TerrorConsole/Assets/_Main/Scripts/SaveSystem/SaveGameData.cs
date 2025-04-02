using System;
using UnityEngine;

namespace TerrorConsole
{
    [Serializable]
    public class SaveGameData
    {
        [SerializeField] private int _gameIndex;
        [SerializeField] private string _currentScene;

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
    }
}
