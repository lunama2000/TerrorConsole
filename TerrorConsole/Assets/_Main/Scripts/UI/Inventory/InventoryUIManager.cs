using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class InventoryUIManager : Singleton<IInventoryUISource>, IInventoryUISource
    {
        [SerializeField] private GameObject _inventoryUI;
        [SerializeField] private UIInventorySlot[] _slots;
        [SerializeField] private Image _previewImage;
        [SerializeField] private TextMeshProUGUI _previewText;
        [SerializeField] private UIController _uIController;

        private LevelState _lastLevelState;

        protected override void Awake()
        {
            base.Awake();
            ResetItemPreview();
        }

        private void InitializeInventory()
        {
            List<ItemInfo> items = SaveSystemManager.Source.GetInventory();
            foreach (UIInventorySlot slot in _slots)
            {
                slot.ResetItem();
            }
            for (int i = 0; i < items.Count; i++)
            {
                _slots[i].SetItem(items[i]);
            }
            UpdateItemPreview(items[0]);
        }
        private void Start()
        {
            InputManager.Source.OnInventoryButton += OnInventoryInput;
        }

        private void OnDestroy()
        {
            InputManager.Source.OnInventoryButton -= OnInventoryInput;
        }

        private void OnInventoryInput()
        {
            if (_inventoryUI.activeSelf)
            {
                OnInventoryClosed();
            }
            else
            {
                if (LevelManager.Source.GetCurrentLevelState() == LevelState.Play)
                {
                    OnInventoryOpened();
                }
            }
        }

        public void ResetItemPreview()
        {
            _previewImage.color = new Color(1, 1, 1, 0);
            _previewText.text = string.Empty;
        }

        public void UpdateItemPreview(ItemInfo item)
        {
            if (item)
            {
                _previewImage.sprite = item.ItemSprite;
                _previewImage.color = Color.white;
                _previewText.text = item.ItemDescription;
            }
            else
            {
                ResetItemPreview();
            }
        }

        public void OnInventoryClosed()
        {
            TooltipsManager.Source.HideAllUITooltips();
            LevelManager.Source.ChangeLevelState(_lastLevelState);
            _uIController.CloseMenu(_uIController);
            TooltipsManager.Source.UnStashUITooltips();
        }

        public void OnInventoryOpened()
        {
            _lastLevelState = LevelManager.Source.GetCurrentLevelState();
            LevelManager.Source.ChangeLevelState(LevelState.InInventory);
            TooltipsManager.Source.StashCurrentUITooltips();
            TooltipsManager.Source.ShowUITooltip(InputActionsInGame.InventoryButton, "Close");
            _uIController.OpenMenu(_uIController);
            InitializeInventory();
            LevelManager.Source.OpenInventory();
        }
    }
}
