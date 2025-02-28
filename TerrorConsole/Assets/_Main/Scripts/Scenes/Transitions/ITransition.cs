using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TerrorConsole
{
    public interface ITransition
    {
        UniTask AnimateIn(float duration, CanvasGroup canvasGroup, RectTransform rectTransform);
        UniTask AnimateOut(float duration, CanvasGroup canvasGroup, RectTransform rectTransform);
    } 
}