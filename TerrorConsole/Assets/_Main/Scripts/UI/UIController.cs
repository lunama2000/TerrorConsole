using UnityEngine;

namespace TerrorConsole
{
    public class UIController : MonoBehaviour
    {
        public void ButtonLoadScene(string sceneName)
        {
            ScreenTransitionManager.Source.TransitionToScene(sceneName);
        }

        public void ButtonPlaySFX()
        {
            AudioManager.Source.PlayUIButtonClickSFX();
        }
    }
}
