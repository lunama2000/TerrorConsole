using UnityEngine;

namespace TerrorConsole
{
    public class HidingPlace : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private float _tooltipOffset = 1;

        private bool _isHiding = false;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                InputManager.Source.OnActivateButton3 += ToggleHiding;
                TooltipsManager.Source.ShowSpriteTooltip(InputActionsInGame.Button3, "Hide", (Vector2)transform.position + Vector2.up * _tooltipOffset);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                InputManager.Source.OnActivateButton3 -= ToggleHiding;
                TooltipsManager.Source.HideSpriteTooltip("Hide");
            }
        }

        private void ToggleHiding()
        {
            _isHiding = !_isHiding;
            if (_isHiding)
            {
                _playerController.Hide();
                TooltipsManager.Source.HideSpriteTooltip("Hide");
                TooltipsManager.Source.ShowSpriteTooltip(InputActionsInGame.Button3, "UnHide", (Vector2)transform.position + Vector2.up * _tooltipOffset);
            }
            else
            {
                _playerController.UnHide();
                TooltipsManager.Source.HideSpriteTooltip("UnHide");
                TooltipsManager.Source.ShowSpriteTooltip(InputActionsInGame.Button3, "Hide", (Vector2)transform.position + Vector2.up * _tooltipOffset);
            }
        }
    }
}
