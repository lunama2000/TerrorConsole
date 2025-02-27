using UnityEngine;

namespace TerrorConsole
{
    public class MainMenuController : MonoBehaviour
    {
        public void ButtonLoadScene(int sceneIndex)
        {
            SceneManager.Source.LoadScene(sceneIndex);
        }
    }
}
