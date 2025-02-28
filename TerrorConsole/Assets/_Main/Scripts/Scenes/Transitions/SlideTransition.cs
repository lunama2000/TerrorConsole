using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace TerrorConsole
{
    public class SlideTransition : ITransition
    {
        public async UniTask AnimateIn(float duration, CanvasGroup canvasGroup, RectTransform rectTransform)
        {
            float screenWidth = Screen.width;
            rectTransform.anchoredPosition = new Vector2(screenWidth, 0);
            canvasGroup.alpha = 1;
            await rectTransform.DOAnchorPosX(0, duration).AsyncWaitForCompletion();
        }

        public async UniTask AnimateOut(float duration, CanvasGroup canvasGroup, RectTransform rectTransform)
        {
            float screenWidth = Screen.width;
            rectTransform.anchoredPosition = new Vector2(0, 0);
            await rectTransform.DOAnchorPosX(screenWidth, duration).AsyncWaitForCompletion();
            canvasGroup.alpha = 0;
        }
    }
}