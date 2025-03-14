using System;
using UnityEngine;
using static TerrorConsole.LevelManager;

namespace TerrorConsole
{
    public interface ILevelManagerSource
    {
        event Action<LevelState> OnLevelStateChange;
        void ChangeLevelState(LevelManager.LevelState newState);
        LevelState GetCurrentState();

    }
}
