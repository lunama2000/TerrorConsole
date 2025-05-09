using UnityEngine;

namespace TerrorConsole
{
    public class TransitionDoors : MonoBehaviour
    {
        [SerializeField] private string _sceneToLoadName;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                LoadScene(_sceneToLoadName);
            }
        }
        public void LoadScene(string sceneName)
        {
            ScreenTransitionManager.Source.TransitionToScene(sceneName);
        }
    }
}
