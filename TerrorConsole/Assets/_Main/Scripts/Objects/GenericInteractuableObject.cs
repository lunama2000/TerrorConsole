using UnityEngine;
using UnityEngine.Events;

namespace TerrorConsole
{
    public class GenericInteractuableObject : MonoBehaviour
    {
        [SerializeField] private InputActionsInGame _inputToUse;
        [SerializeField] private string _tooltipToDisplay;
        public UnityEvent OnInteracted;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                InputManager.Source.SuscribeToInputActionsInGame(_inputToUse, OnInteractedInput);
                TooltipsManager.Source.ShowSpriteTooltip(_inputToUse, _tooltipToDisplay, (Vector2)transform.position + Vector2.up);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                InputManager.Source.UnSuscribeToInputActionsInGame(_inputToUse, OnInteractedInput);
                TooltipsManager.Source.HideSpriteTooltip(_tooltipToDisplay);
            }
        }

        private void OnInteractedInput()
        {
            OnInteracted.Invoke();
        }

    }
}
