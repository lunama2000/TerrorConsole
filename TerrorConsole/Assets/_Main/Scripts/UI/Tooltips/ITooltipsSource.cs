using UnityEngine;

namespace TerrorConsole
{
    public interface ITooltipsSource
    {
        void ShowUITooltip(InputActionsInGame inputType, string actionName);
        void ShowSpriteTooltip(InputActionsInGame inputType, string actionName, Vector2 position);
        void HideSpriteTooltip(string actionName);
        void HideAllSpriteTooltips();

        public Sprite GetKeycodeIcon(KeyCode keycode);

        void HideUITooltip(string actionName);

        void HideAllUITooltips();
        public Sprite GetActionInGameIcon(InputActionsInGame actionsInGame);

        void StashCurrentUITooltips();
        void UnStashUITooltips();
    }
}
