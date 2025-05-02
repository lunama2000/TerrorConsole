using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class InventoryUIController : Singleton<IInventoryUISource>, IInventoryUISource
    {
        [SerializeField] private GameObject _inventoryUI;
        [SerializeField] private UIInventorySlot[] _slots;
        [SerializeField] private Image _previewImage;
        [SerializeField] private TextMeshProUGUI _previewText;
        [SerializeField] private GameObject _defaultSelected;

        protected override void Awake()
        {
            base.Awake();
            ResetItemPreview();
        }

        private void InitializeInventory()
        {
            List<ItemInfo> items = SaveSystemManager.Source.GetLoadedGame().GetInventory();
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
            _inventoryUI.SetActive(!_inventoryUI.activeSelf);
            if (_inventoryUI.activeSelf)
            {
                LevelManager.Source.PauseLevel();
                InitializeInventory();
                EventSystem.current.SetSelectedGameObject(_defaultSelected);
            }
            else
            {
                LevelManager.Source.PlayLevel();
            }
        }

        public void ResetItemPreview()
        {
            _previewImage.color = new Color(1,1, 1, 0);
            _previewText.text = string.Empty;
        }

        public void UpdateItemPreview(ItemInfo item)
        {
            if (item)
            {
                _previewImage.sprite = item.itemSprite;
                _previewImage.color = Color.white;
                _previewText.text = item.itemDescription;
            }
            else
            {
                ResetItemPreview();
            }
        }
    }
}
