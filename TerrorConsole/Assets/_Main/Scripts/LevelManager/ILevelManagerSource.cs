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
        void OpenInventory();
        LevelState GetCurrentState();

        public void AddOrUpdateLevelEvent(string eventName, bool eventState);
        public bool GetEventState(string uniqueEventKey);

        public void AddOrUpdateLevelParameter(string parameterName, int parameterValue);
        public int GetLevelParameterValue(string parameterName);

        void SetRespawnPosition(Vector3 newPosition);
        Vector3 GetRespawnPosition();

        void PlayerCaptured();
        void RespawnPlayer();

        LevelState GetCurrentLevelState();
    }
}
