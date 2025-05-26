using UnityEngine;

namespace TerrorConsole
{
    public class Credits : MonoBehaviour
    {
        [SerializeField] private float _scrollSpeed = 80f;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Transform _resetThreshold;
        
        private float _startAnchoredPositionY;

        private void Start()
        {
            _startAnchoredPositionY = _rectTransform.anchoredPosition.y;
        }

        private void Update()
        {
            _rectTransform.anchoredPosition += new Vector2(0, _scrollSpeed * Time.deltaTime);
            
            if (_rectTransform.position.y > _resetThreshold.position.y)
            {
                _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _startAnchoredPositionY);
            }
        }
    }
}
