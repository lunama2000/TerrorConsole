using System;

namespace TerrorConsole
{
    public interface ILevelManagerSource
    {
        event Action<LevelState> OnLevelStateChange;
        void ChangeLevelState(LevelState newState);
        LevelState GetCurrentState();
    }
}
