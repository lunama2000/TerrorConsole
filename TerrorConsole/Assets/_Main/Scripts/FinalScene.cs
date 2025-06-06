using UnityEngine;

namespace TerrorConsole
{
    public class FinalScene : MonoBehaviour
    {
        public void ChageState()
        {
            LevelManager.Source.ChangeLevelState(LevelState.Cinematic);

        }

        public void HideUI()
        {
            TooltipsManager.Source.HideAllUITooltips();
        }
    }
}
