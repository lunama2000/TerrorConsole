using UnityEngine;

namespace TerrorConsole
{
    public class SceneManager : Singleton<SceneManager>
    {
        [SerializeField] TransitionController _transitionController;

        public void LoadScene(int sceneIndex)
        {
            _transitionController.TransitionToScene(sceneIndex);
        }
    }
}
