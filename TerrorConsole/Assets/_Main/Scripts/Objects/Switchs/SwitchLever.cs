using UnityEngine;

namespace TerrorConsole
{
    public class SwitchLever : SwitchObject
    {
        private bool _isPlayerColliding;
        [SerializeField] Animator _animator;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                InputManager.Source.OnActivateButton1 += AlternateState;
                TooltipsManager.Source.ShowSpriteTooltip(InputActionsInGame.Button1, "Interact",(Vector2)collision.transform.position+Vector2.up);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                InputManager.Source.OnActivateButton1 -= AlternateState;
                TooltipsManager.Source.HideSpriteTooltip("Interact");
            }
        }

        protected override void AlternateState()
        {
            base.AlternateState();
            _animator.SetTrigger("ChangeState");
        }
    }
}
