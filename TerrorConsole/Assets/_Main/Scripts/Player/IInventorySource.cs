using UnityEngine;

namespace TerrorConsole
{
    public interface IInventorySource
    {
        public void AddItemToInventory(string itemName);
        public void RemoveItemFromInventory(string itemName);

        public bool IsItemInInventory(string itemName);
    }
}
