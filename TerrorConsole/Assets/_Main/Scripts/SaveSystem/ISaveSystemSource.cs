using UnityEngine;

namespace TerrorConsole
{
    public interface ISaveSystemSource
    {
        public void SaveGame(int fileIndex, SaveGameData data);
        public void SaveCurrentGame();
        public SaveGameData LoadGame(int fileIndex);
        public void DeleteGame(int fileIndex);
        public int GetLastLoadedFileIndex();
        public bool CheckIfFileExist(int fileIndex);
        public SaveGameData GetGameDataByIndex(int fileIndex);
        public SaveGameData GetLoadedGame();
    }
}
