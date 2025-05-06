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
        Cinematic
    }

    public partial class LevelManager : Singleton<ILevelManagerSource>, ILevelManagerSource
    {
        [SerializeField] private int levelNumber;

        public event Action<LevelState> OnLevelStateChange;
       
        public UnityEvent OnPlayerCaptured { get; set; } = new UnityEvent();
        public UnityEvent OnPlayerRespawn { get; set; } = new UnityEvent();

        [SerializeField] private LevelState currentLevelState;

        public void ChangeLevelState(LevelState newState)
        {
            currentLevelState = newState;
            OnLevelStateChange?.Invoke(newState);
        }

        public void PauseLevel()
        {
            ChangeLevelState(LevelState.Pause);
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
        public void SubscribeToPlayerCaptured(UnityAction callback)
        {
            OnPlayerCaptured.AddListener(callback);
        }

        public void UnsubscribeFromPlayerCaptured(UnityAction callback)
        {
            OnPlayerCaptured.RemoveListener(callback);
        }

        public void SubscribeToPlayerRespawn(UnityAction callback)
        {
            OnPlayerRespawn.AddListener(callback);
        }

        public void UnsubscribeFromPlayerRespawn(UnityAction callback)
        {
            OnPlayerRespawn.RemoveListener(callback);
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
