using UnityEngine;

namespace TerrorConsole
{
    public class SpriteTooltip : Tooltip
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private TextMesh _textMesh;
        protected override void DisplayIcon()
        {
            _spriteRenderer.sprite = _inputSprite;
            _textMesh.text = _actionName;
        }
    }
}
