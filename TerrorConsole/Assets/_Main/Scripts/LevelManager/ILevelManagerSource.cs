using System;

namespace TerrorConsole
{
    public interface ILevelManagerSource
    {
        event Action<LevelState> OnLevelStateChange;
        event Action OnPlayerCaptured;
        event Action OnPlayerRespawn;
        
        void ChangeLevelState(LevelState newState);
        void PlayLevel();
        void PauseLevel();
        LevelState GetCurrentState();

        public void AddOrUpdateLevelEvent(string eventName, bool eventState);
        public bool GetEventState(string uniqueEventKey);

        void PlayerCaptured();
        void RespawnPlayer();
    }
}
