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
        [SerializeField] RectTransform contentPanel;

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
            Vector2 targetPosition = (Vector2)_scrollRect.transform.InverseTransformPoint(contentPanel.position) - (Vector2)_scrollRect.transform.InverseTransformPoint(transform.position);

            targetPosition -= new Vector2(0, _myRectTransform.rect.height);
            contentPanel.DOAnchorPos(targetPosition, 0.3f).SetEase(Ease.OutCubic);
        }
    }
}
