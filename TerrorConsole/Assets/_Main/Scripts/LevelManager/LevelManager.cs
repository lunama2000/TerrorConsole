using System;
using UnityEngine;

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

        [SerializeField] private LevelState currentLevelState;

        private SaveLevelData _saveLevelData;

        protected override void Awake()
        {
            base.Awake();
            _saveLevelData = SaveSystemManager.Source.GetLoadedGame().GetLevelData(levelNumber);
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

        public void PlayLevel()
        {
            ChangeLevelState(LevelState.Play);
        }

        public LevelState GetCurrentState()
        {
            return currentLevelState;
        }

        public SaveLevelData GetSaveLevelData()
        {
            return _saveLevelData;
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
