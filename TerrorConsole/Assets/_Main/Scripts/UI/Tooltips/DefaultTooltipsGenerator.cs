using System;
using System.Collections.Generic;
using UnityEngine;

namespace TerrorConsole
{
    public class DefaultTooltipsGenerator : MonoBehaviour
    {
        [Serializable]
        struct TooltipInfo
        {
            public InputActionsInGame inputAction;
            public string actionName;
        }

        [SerializeField] List<TooltipInfo> tooltipsToDisplay = new List<TooltipInfo>();
        private void Start()
        {
            foreach (TooltipInfo tooltip in tooltipsToDisplay)
            {
                TooltipsManager.Source.ShowTooltip(tooltip.inputAction, tooltip.actionName);
            }
        }
    }
}
