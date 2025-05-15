using UnityEngine;

namespace TerrorConsole
{
    public class HidingPlace : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        private bool _isHiding = false;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                InputManager.Source.OnActivateButton2 += ToggleHiding;
                TooltipsManager.Source.ShowSpriteTooltip(InputActionsInGame.Button2, "Hide", (Vector2)other.transform.position + Vector2.up);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                InputManager.Source.OnActivateButton2 -= ToggleHiding;
                TooltipsManager.Source.HideSpriteTooltip("Hide");
            }
        }

        private void ToggleHiding()
        {
            _isHiding = !_isHiding;
            if (_isHiding)
            {
                _playerController.Hide();
            }
            else
            {
                _playerController.UnHide();
            }
        }
    }
}
