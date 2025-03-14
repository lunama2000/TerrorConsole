using System;
using UnityEngine;

namespace TerrorConsole
{
    public class LevelManager : Singleton<ILevelManagerSource>, ILevelManagerSource
    {

        public event Action<LevelState> OnLevelStateChange;

        [SerializeField] private LevelState currentLevelState;
        
        public enum LevelState
        {
            Play,
            Pause,
            InDialogue,
            Cinematic
        }

        public void ChangeLevelState(LevelState newState)
        {
            currentLevelState = newState;
            OnLevelStateChange?.Invoke(newState);
        }

        public LevelState GetCurrentState()
        {
            return currentLevelState;
        }

        [ContextMenu("DEBUG_ChangeState_Play")]
        public void DEBUG_ChangeState_Play()
        {
            ChangeLevelState(LevelState.Play);
        }

        [ContextMenu ("DEBUG_ChangeState_Pause")]
        public void DEBUG_ChangeState_Pause()
        {
            ChangeLevelState(LevelState.Pause);
        }
    }
}
