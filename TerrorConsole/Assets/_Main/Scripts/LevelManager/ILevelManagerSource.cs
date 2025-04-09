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
        public void AddOrUpdateLevelEvent(string eventName, bool eventState);
        public bool GetEventState(string uniqueEventKey);
    }
}
