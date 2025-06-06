using UnityEngine;

namespace TerrorConsole
{
    public class FinalScene : MonoBehaviour
    {
        private void Awake()
        {
            LevelManager.Source.ChangeLevelState(LevelState.Cinematic);
        }
    }
}
