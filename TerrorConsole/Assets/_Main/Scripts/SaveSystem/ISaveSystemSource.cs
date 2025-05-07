using System.Collections.Generic;

namespace TerrorConsole
{
    public interface ISaveSystemSource
    {
        ISaveGameData LoadGame(int fileIndex);
        void DeleteGame(int fileIndex);
        int GetLastLoadedFileIndex();
        bool CheckIfFileExist(int fileIndex);
        ISaveGameData GetGameDataByIndex(int fileIndex);

        void SetInventory(List<ItemInfo> newInventory);
        List<ItemInfo> GetInventory();

        void AddOrUpdateLevelEvent(int levelNumber, string eventName, bool eventState);
        bool GetEventState(int levelNumber, string eventName);

        void SaveLanguageCode(string languageCode);
    }
}
