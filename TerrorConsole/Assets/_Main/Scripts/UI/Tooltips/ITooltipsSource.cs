using UnityEngine;

namespace TerrorConsole
{
    public interface ITooltipsSource
    {
        void ShowTooltip(InputActionsInGame inputType, string actionName);
        void ShowTooltip(KeyCode inputKeyCode, string actionName);

        public Sprite GetKeycodeIcon(KeyCode keycode);

        void HideTooltip(string actionName);

        void HideAll();
        public Sprite GetActionInGameIcon(InputActionsInGame actionsInGame);

        void StashCurrentTooltips();
        void UnStashTooltips();
    }
}
