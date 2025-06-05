using System;
using UnityEngine;

namespace TerrorConsole
{
    public interface IInventorySource
    {
        public void AddItemToInventory(ItemInfo newItem);
        public void RemoveItemFromInventory(ItemInfo newItem);
        public bool IsItemInInventory(ItemInfo newItem);
        public void SaveInventory();
        Action OnInventoryUpdated { get; set; }
    }
}
