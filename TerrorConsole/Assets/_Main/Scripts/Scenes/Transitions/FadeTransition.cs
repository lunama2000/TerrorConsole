using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace TerrorConsole
{
    public class FadeTransition : ITransition
    {
        public async UniTask AnimateIn(float duration, CanvasGroup canvasGroup, RectTransform rectTransform)
        {
            await canvasGroup.DOFade(1, duration).AsyncWaitForCompletion();
        }

        public async UniTask AnimateOut(float duration, CanvasGroup canvasGroup, RectTransform rectTransform)
        {
            await canvasGroup.DOFade(0, duration).AsyncWaitForCompletion();
        }
    }   
}