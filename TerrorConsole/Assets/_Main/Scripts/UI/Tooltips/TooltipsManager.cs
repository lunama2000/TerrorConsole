using System.Collections.Generic;
using UnityEngine;

namespace TerrorConsole
{
    public class TooltipsManager : Singleton<ITooltipsSource>, ITooltipsSource
    {
        [Header("Sprite Tooltips")]
        [SerializeField] private GameObject _spriteToolTipPrefab;
        private Dictionary<string, GameObject> _currentSpriteTooltips = new Dictionary<string, GameObject>();

        [Header("UI Tooltips")]
        [SerializeField] private GameObject _uIToolTipsHolder;
        [SerializeField] private GameObject _uIToolTipPrefab;

        private Dictionary<string, GameObject> _currentUITooltips = new Dictionary<string, GameObject>();
        private Dictionary<string, GameObject> _stashedUITooltips = new Dictionary<string, GameObject>();


        [Header("Icons Database")]
        [SerializeField] private InputIconsDatabase _iconsDatabase;

        public void HideAllUITooltips()
        {
            foreach (GameObject tooltip in _currentUITooltips.Values)
            {
                Destroy(tooltip);
            }
            _currentUITooltips.Clear();
        }

        public void HideUITooltip(string actionName)
        {
            if (!_currentUITooltips.ContainsKey(actionName))
                return;

            Destroy(_currentUITooltips[actionName]); //TO DO: Implement pooling
            _currentUITooltips.Remove(actionName);
        }

        public void ShowUITooltip(InputActionsInGame inputType, string actionName)
        {
            if (_currentUITooltips.ContainsKey(actionName))
                return;

            UITooltip newTooltip = Instantiate(_uIToolTipPrefab,_uIToolTipsHolder.transform).GetComponent<UITooltip>();
            newTooltip.Initialize(inputType, actionName);
            _currentUITooltips.Add(actionName, newTooltip.gameObject);
        }

        public Sprite GetActionInGameIcon(InputActionsInGame actionsInGame)
        {
            return _iconsDatabase.GetKeyCodeIcon(InputManager.Source.GetActionInGameKeyCode(actionsInGame));
        }

        public Sprite GetKeycodeIcon(KeyCode keycode)
        {
            return _iconsDatabase.GetKeyCodeIcon(keycode);
        }

        public void StashCurrentUITooltips()
        {
            foreach (KeyValuePair<string, GameObject> tooltip in _currentUITooltips)
            {
                _stashedUITooltips.Add(tooltip.Key, tooltip.Value);
                tooltip.Value.SetActive(false);
            }
            _currentUITooltips.Clear();
        }

        public void UnStashUITooltips()
        {
            foreach (KeyValuePair<string, GameObject> tooltip in _stashedUITooltips)
            {
                _currentUITooltips.Add(tooltip.Key, tooltip.Value);
                tooltip.Value.SetActive(true);
            }
            _stashedUITooltips.Clear();
        }

        public void ShowSpriteTooltip(InputActionsInGame inputType, string actionName, Vector2 position)
        {
            if (_currentSpriteTooltips.ContainsKey(actionName))
                return;

            SpriteTooltip newTooltip = Instantiate(_spriteToolTipPrefab, position,Quaternion.identity, transform).GetComponent<SpriteTooltip>();
            newTooltip.Initialize(inputType, actionName);
            _currentSpriteTooltips.Add(actionName, newTooltip.gameObject);
        }

        public void HideSpriteTooltip(string actionName)
        {
            if (!_currentSpriteTooltips.ContainsKey(actionName))
                return;

            Destroy(_currentSpriteTooltips[actionName]); //TO DO: Implement pooling
            _currentSpriteTooltips.Remove(actionName);
        }

        public void HideAllSpriteTooltips()
        {
            foreach (GameObject tooltip in _currentSpriteTooltips.Values)
            {
                Destroy(tooltip);
            }
            _currentSpriteTooltips.Clear();
        }
    }
}
