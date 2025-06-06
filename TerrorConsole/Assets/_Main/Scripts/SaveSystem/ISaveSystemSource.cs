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
        void SetCurrentScene(string currentScene);
        List<ItemInfo> GetInventory();

        void AddOrUpdateLevelEvent(int levelNumber, string eventName, bool eventState);
        bool GetEventState(int levelNumber, string eventName);

        void AddOrUpdateLevelParameter(int levelNumber, string parameterName, int parameterValue);
        int GetLevelParameterValue(int levelNumber, string parameterName);

        void SaveLanguageCode(string languageCode);
        string GetSavedLanguageCode();

        void SaveSFXVolume(float newSFX);

        void SaveMusicVolume(float newMusic);
        void SaveResolution(int newIndex);

        float GetSavedMusicVolume();
        float GetSavedSFXVolume();

        int GetSavedResolution();
    }
}
