using System;
using System.Collections.Generic;
using UnityEngine;

namespace TerrorConsole
{
    public class Inventory : Singleton<IInventorySource>, IInventorySource
    {
        [SerializeField] private List<ItemInfo> _itemsList;

        public Action OnInventoryUpdated { get; set; }

        private void Start()
        {
            _itemsList = SaveSystemManager.Source.GetInventory();
            if (_itemsList == null)
            {
                _itemsList = new List<ItemInfo>();
            }
        }

        public void AddItemToInventory(ItemInfo newItem)
        {
            _itemsList.Add(newItem);
            print($"{newItem.ItemName} has been added to the inventory");
            SaveInventory();
        }

        public bool IsItemInInventory(ItemInfo newItem)
        {
            return _itemsList.Contains(newItem);
        }

        public void RemoveItemFromInventory(ItemInfo newItem)
        {
            _itemsList.Remove(newItem);
            print($"{newItem} has been removed from the inventory");
            SaveInventory();
        }

        public void SaveInventory()
        {
            SaveSystemManager.Source.SetInventory(_itemsList);
            OnInventoryUpdated?.Invoke();
        }
    }
}
