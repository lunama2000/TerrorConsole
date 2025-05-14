using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace TerrorConsole
{
    public class AutoScrollDropdownItem : MonoBehaviour, ISelectHandler
    {
        [SerializeField] RectTransform _myRectTransform;
        [SerializeField] Toggle _myToggle;
        [SerializeField] ScrollRect _scrollRect;

        private void Start()
        {
            if(_myToggle.isOn)
                UpdateScrollPosition();
        }

        public void OnSelect(BaseEventData eventData)
        {
            UpdateScrollPosition();
        }

        private void UpdateScrollPosition()
        {
            float targetY = 1f - (float)(_myRectTransform.GetSiblingIndex() - 1) / (_scrollRect.content.childCount - 2);
            Vector2 startPos = _scrollRect.normalizedPosition;

            DOTween.To(
            () => _scrollRect.normalizedPosition.y,
            y => _scrollRect.normalizedPosition = new Vector2(0f, y),
            targetY,
            0.3f
            );
        }
    }
}
