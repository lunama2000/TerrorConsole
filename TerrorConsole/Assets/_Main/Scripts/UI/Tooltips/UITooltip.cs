using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class UITooltip : MonoBehaviour
    {
        [SerializeField] private Image _inputImage;
        [SerializeField] private TextMeshProUGUI _actionName;
        [SerializeField] private InputActionsInGame _inputToDisplay;

        private void OnEnable()
        {
            InputManager.Source.OnInputTypeChange += UpdateIconWithNewInput;
        }

        public void Initialize(InputActionsInGame inputToDisplay, string actionName)
        {
            _inputToDisplay = inputToDisplay;
            _actionName.text = actionName;
            UpdateIcon();
        }

        private void UpdateIcon()
        {
            _inputImage.sprite = TooltipsManager.Source.GetActionInGameIcon(_inputToDisplay);
        }

        public void UpdateIconWithNewInput(InputType newInputType)
        {
            UpdateIcon();
        }
    }
}
