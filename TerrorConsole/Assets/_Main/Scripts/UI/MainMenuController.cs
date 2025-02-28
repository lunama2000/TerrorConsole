using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TerrorConsole
{
    public class MainMenuController : MonoBehaviour
    {
        public void TransitionToScene(int sceneIndex)
        {
            SceneManager.Source.TransitionToScene(sceneIndex).Forget();
        }
    }
}
