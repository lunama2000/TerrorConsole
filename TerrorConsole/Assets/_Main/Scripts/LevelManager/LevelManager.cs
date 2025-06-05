using System;
using UnityEngine;
using UnityEngine.Events;

namespace TerrorConsole
{
    public enum LevelState
    {
        Play,
        Pause,
        InDialogue,
        Cinematic,
        Hiding,
        InInventory,
        PlayerCaptured,
        ReadingNote,
        InTransition,
        BossPhase
    }

    public partial class LevelManager : Singleton<ILevelManagerSource>, ILevelManagerSource
    {
        [SerializeField] private int levelNumber;

        public event Action<LevelState> OnLevelStateChange;

        public event Action OnPlayerCaptured;
        public event Action OnPlayerRespawn;

        [SerializeField] private LevelState currentLevelState;
        
        private Vector3 _currentRespawnPosition;

        private void Start()
        {
            ScreenTransitionManager.Source.SuscribeToLevelEvents();
            ChangeLevelState(LevelState.Play);
        }

        public void ChangeLevelState(LevelState newState)
        {
            currentLevelState = newState;
            OnLevelStateChange?.Invoke(newState);
        }

        public void PauseLevel()
        {
            ChangeLevelState(LevelState.Pause);
        }
        public void OpenInventory()
        {
            ChangeLevelState(LevelState.InInventory);
        }

        public void PlayLevel()
        {
            ChangeLevelState(LevelState.Play);
        }

        public LevelState GetCurrentState()
        {
            return currentLevelState;
        }

        public void AddOrUpdateLevelEvent(string eventName, bool eventState)
        {
            SaveSystemManager.Source.AddOrUpdateLevelEvent(levelNumber, eventName, eventState);
        }

        public bool GetEventState(string uniqueEventKey)
        {
            return SaveSystemManager.Source.GetEventState(levelNumber, uniqueEventKey);
        }

        public void AddOrUpdateLevelParameter(string parameterName, int parameterValue)
        {
            SaveSystemManager.Source.AddOrUpdateLevelParameter(levelNumber, parameterName, parameterValue);
        }

        public int GetLevelParameterValue(string parameterName)
        {
            return SaveSystemManager.Source.GetLevelParameterValue(levelNumber, parameterName);
        }

        public void SetRespawnPosition(Vector3 newPosition)
        {
            _currentRespawnPosition = newPosition;
        }
        
        public Vector3 GetRespawnPosition()
        {
            return _currentRespawnPosition;
        }
        
        public void PlayerCaptured()
        {
            ChangeLevelState(LevelState.PlayerCaptured);
            OnPlayerCaptured?.Invoke();
        }

        public void RespawnPlayer()
        {
            OnPlayerRespawn?.Invoke();
        }

        public LevelState GetCurrentLevelState()
        {
            return currentLevelState;
        }
    }

    #if UNITY_EDITOR
    public partial class LevelManager
    {
        [ContextMenu("DEBUG_ChangeState_Play")]
        public void DEBUG_ChangeState_Play()
        {
            ChangeLevelState(LevelState.Play);
        }

        [ContextMenu("DEBUG_ChangeState_Pause")]
        public void DEBUG_ChangeState_Pause()
        {
            ChangeLevelState(LevelState.Pause);
        }
    }
    #endif
}
