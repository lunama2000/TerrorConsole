using UnityEngine;

namespace TerrorConsole
{
    public class MainMenuController : MonoBehaviour
    {
        public void ButtonLoadScene(string sceneName)
        {
            ScreenTransitionManager.Source.TransitionToScene(sceneName);
        }
    }
}
