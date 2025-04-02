using UnityEngine;

namespace TerrorConsole
{
    public interface ISaveSystemSource
    {
        public void SaveGame(int fileIndex);
        public void SaveCurrentGame();
        public void SaveProgress(int currentLevel, string inventory);
        public void LoadGame(int fileIndex);
        public void DeleteGame(int fileIndex);
        public int GetLastLoadedFileIndex();
        public bool CheckIfFileExist(int fileIndex);
    }
}
