using System;

namespace TerrorConsole
{
    public interface ILevelManagerSource
    {
        event Action<LevelState> OnLevelStateChange;
        void ChangeLevelState(LevelState newState);
        public void PlayLevel();
        public void PauseLevel();
        LevelState GetCurrentState();
        SaveLevelData GetSaveLevelData();
    }
}
