using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TerrorConsole
{
    public class SceneManager : Singleton<SceneManager>
    {
        [Header("COMPONENTS")]
        [SerializeField] private CanvasGroup _transitionCanvas;
        [SerializeField] private RectTransform _foregroundPanel;
        
        [Header("TRANSITION PARAMETERS")]
        [SerializeReference] private ITransition _transitionType;
        [SerializeField] private float _transitionDuration = 1;
        
        public async UniTask TransitionToScene(int sceneIndex)
        {
            await _transitionType.AnimateIn(_transitionDuration, _transitionCanvas, _foregroundPanel);
            await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);
            await _transitionType.AnimateOut(_transitionDuration, _transitionCanvas, _foregroundPanel);
        }
    }
}
