using UnityEngine;

namespace TerrorConsole
{
    public interface ITooltipsSource
    {
        void ShowTooltip(InputActionsInGame inputType, string actionName);

        void HideTooltip(string actionName);

        void HideAll();
        public Sprite GetActionInGameIcon(InputActionsInGame actionsInGame);
    }
}
