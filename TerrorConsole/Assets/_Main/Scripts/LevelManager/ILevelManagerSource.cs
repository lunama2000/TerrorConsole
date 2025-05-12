using System;
using UnityEngine;

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

        void SetRespawnPosition(Vector3 newPosition);
        Vector3 GetRespawnPosition();

        void PlayerCaptured();
        void RespawnPlayer();

        LevelState GetCurrentLevelState();
    }
}
