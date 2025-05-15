using UnityEngine;

namespace TerrorConsole
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private InputActionsInGame _inputToDisplay;
        protected string _actionName;
        protected Sprite _inputSprite;

        private void OnEnable()
        {
            InputManager.Source.OnInputTypeChange += UpdateIconWithNewInput;
        }
        private void OnDestroy()
        {
            InputManager.Source.OnInputTypeChange -= UpdateIconWithNewInput;
        }

        public void Initialize(InputActionsInGame inputToDisplay, string actionName)
        {
            _inputToDisplay = inputToDisplay;
            _actionName = actionName;
            UpdateIcon();
        }
        private void UpdateIcon()
        {
            _inputSprite = TooltipsManager.Source.GetActionInGameIcon(_inputToDisplay);
            DisplayIcon();
        }

        protected virtual void DisplayIcon()
        {
            print("Please set a Sprite Renderer Or Image to update the Icon");
        }

        public void UpdateIconWithNewInput(InputType newInputType)
        {
            UpdateIcon();
        }
    }
}
