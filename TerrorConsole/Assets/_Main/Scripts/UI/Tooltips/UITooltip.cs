using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class UITooltip : Tooltip
    {
        [SerializeField] private Image _inputImage;
        [SerializeField] private TextMeshProUGUI _actionText;

        protected override void DisplayIcon()
        {
            _inputImage.sprite = _inputSprite;
            _actionText.text = _actionName;
        }
    }
}
