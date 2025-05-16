using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TerrorConsole
{
    public class ItemsCore : Singleton<IInventorySource>, IInventorySource
    {
        [SerializeField] private List<ItemInfo> _itemsList;
        [SerializeField] private List<GameObject> _itemsCore;
        private List<GameObject> _currentItems = new List<GameObject>();

        
        public Action OnInventoryUpdated { get; set; }

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

        private void AllItemsPicked(GameObject ItemPicked)
        {
            _currentItems.Add(ItemPicked);
            CheckItems();
        }

        private void CheckItems()
        {
            if (_currentItems.Count != _itemsCore.Count)
                return;

            for (int i = 0; i < _itemsCore.Count; i++)
            {
                if (_currentItems[i] != _itemsCore[i])
                {
                    
                }
            }
        }
    }
}
