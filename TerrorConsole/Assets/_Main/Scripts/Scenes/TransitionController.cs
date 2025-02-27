using DG.Tweening;
using UnityEngine;

namespace TerrorConsole
{
    public class TransitionController : MonoBehaviour
    {


        [Header("COMPONENTS")]
        [SerializeField] private CanvasGroup _mainCanvasGroup;
        [SerializeField] private GameObject _backgroundPanel;


        [SerializeField]
        private enum TRANSITION_TYPE
        {
            Fade,
            Slide
        }
        [Header("CONFIGURATIONS")]
        [SerializeField] bool _startWithTransition = true;
        [SerializeField] private TRANSITION_TYPE _transitionType;
        [SerializeField] private float _transitionDuration = 1;

        bool _loadScene;
        private int _sceneIndexToLoad;
        private void Start()
        {
            if (_startWithTransition)
            {
                StartTransition(true);
            }
        }

        private void StartTransition(bool fadeIn)
        {
            switch (_transitionType)
            {
                case TRANSITION_TYPE.Fade: TransitionFade(fadeIn); break;
                case TRANSITION_TYPE.Slide: TransitionSlide(fadeIn); break;
            }
        }

        
        private void InTransitionEnded()
        {
            DeactivateTransitionCanvas();
        }
        private void OutTransitionEnded()
        {
            if (_loadScene)
            {
                _loadScene = false;
                LoadScene();
            }
        }


        public void TransitionToScene(int sceneIndex)
        {
            StartTransition(false);
            _sceneIndexToLoad = sceneIndex;
            _loadScene = true;
        }
        void LoadScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneIndexToLoad);
            StartTransition(true); //Suponiendo que la escena no tarda nada en cargar | Necesito investigar como verificar cuando una escena termino de cargar
        }
        private void ActivateTransitionCanvas()
        {

            _mainCanvasGroup.alpha = 1;
            _mainCanvasGroup.gameObject.SetActive(true);
        }
        private void DeactivateTransitionCanvas()
        {
            _mainCanvasGroup.gameObject.SetActive(false);
        }

        #region TransitionTypes
        private void TransitionFade(bool fadeIn = true)
        {
            ActivateTransitionCanvas();
            if (fadeIn)
            {
                DOTween.To(() => _mainCanvasGroup.alpha, x => _mainCanvasGroup.alpha = x, 0, _transitionDuration).OnComplete(InTransitionEnded);
            }
            else
            {
                _mainCanvasGroup.alpha = 0;
                DOTween.To(() => _mainCanvasGroup.alpha, x => _mainCanvasGroup.alpha = x, 1, _transitionDuration).OnComplete(OutTransitionEnded);
            }
        }
        private void TransitionSlide(bool fadeIn = true)
        {

            ActivateTransitionCanvas();
            RectTransform _mainCanvasRect = _mainCanvasGroup.GetComponent<RectTransform>();
            float _screenWidth = _mainCanvasRect.rect.width;
            float _screenHeight = _mainCanvasRect.rect.height;
            if (fadeIn)
            {
                _backgroundPanel.transform.localPosition = new Vector3(0, 0, 0);
                _backgroundPanel.transform.DOLocalMoveX(_screenWidth, _transitionDuration).OnComplete(InTransitionEnded);

            }
            else
            {
                _backgroundPanel.transform.localPosition = new Vector3(_screenWidth, 0, 0);
                _backgroundPanel.transform.DOLocalMoveX(0, _transitionDuration).OnComplete(OutTransitionEnded);

            }
        }
        #endregion
    }
}
