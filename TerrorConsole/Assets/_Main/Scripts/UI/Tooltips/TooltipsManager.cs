using System.Collections.Generic;
using UnityEngine;

namespace TerrorConsole
{
    public class TooltipsManager : Singleton<ITooltipsSource>, ITooltipsSource
    {
        [SerializeField] private GameObject _toolTipsHolder;
        [SerializeField] private GameObject _toolTipPrefab;

        private Dictionary<string, GameObject> _currentTooltips = new Dictionary<string, GameObject>();
        private Dictionary<string, GameObject> _stashedTooltips = new Dictionary<string, GameObject>();

        [SerializeField] private InputIconsDatabase _iconsDatabase;

        public void HideAll()
        {
            foreach (GameObject tooltip in _currentTooltips.Values)
            {
                Destroy(tooltip);
            }
            _currentTooltips.Clear();
        }

        public void HideTooltip(string actionName)
        {
            if (!_currentTooltips.ContainsKey(actionName))
                return;

            Destroy(_currentTooltips[actionName]); //TO DO: Implement pooling
            _currentTooltips.Remove(actionName);
        }

        public void ShowTooltip(InputActionsInGame inputType, string actionName)
        {
            if (_currentTooltips.ContainsKey(actionName))
                return;

            UITooltip newTooltip = Instantiate(_toolTipPrefab,_toolTipsHolder.transform).GetComponent<UITooltip>();
            newTooltip.Initialize(inputType, actionName);
            _currentTooltips.Add(actionName, newTooltip.gameObject);
        }

        public void ShowTooltip(KeyCode inputKeyCode, string actionName)
        {
            if (_currentTooltips.ContainsKey(actionName))
                return;

            UITooltip newTooltip = Instantiate(_toolTipPrefab, _toolTipsHolder.transform).GetComponent<UITooltip>();
            newTooltip.Initialize(inputKeyCode, actionName);
            _currentTooltips.Add(actionName, newTooltip.gameObject);
        }

        public Sprite GetActionInGameIcon(InputActionsInGame actionsInGame)
        {
            return _iconsDatabase.GetKeyCodeIcon(InputManager.Source.GetActionInGameKeyCode(actionsInGame));
        }

        public Sprite GetKeycodeIcon(KeyCode keycode)
        {
            return _iconsDatabase.GetKeyCodeIcon(keycode);
        }

        public void StashCurrentTooltips()
        {
            foreach (KeyValuePair<string, GameObject> tooltip in _currentTooltips)
            {
                _stashedTooltips.Add(tooltip.Key, tooltip.Value);
                tooltip.Value.SetActive(false);
            }
            _currentTooltips.Clear();
        }

        public void UnStashTooltips()
        {
            foreach (KeyValuePair<string, GameObject> tooltip in _stashedTooltips)
            {
                _currentTooltips.Add(tooltip.Key, tooltip.Value);
                tooltip.Value.SetActive(true);
            }
            print(_currentTooltips.Count);
            _stashedTooltips.Clear();
        }
    }
}
