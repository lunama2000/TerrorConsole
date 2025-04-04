using System.Collections.Generic;
using UnityEngine;

namespace TerrorConsole
{
    public class Inventory : Singleton<IInventorySource>, IInventorySource
    {
        [SerializeField] private List<string> _itemsList;

        protected override void Awake()
        {
            base.Awake();
            _itemsList = SaveSystemManager.Source.GetLoadedGame().GetInventory();
            if (_itemsList == null)
            {
                _itemsList = new List<string>();
            }
        }

        public void AddItemToInventory(string itemName)
        {
            _itemsList.Add(itemName);
            print($"{itemName} has been added to the inventory");
            SaveInventory();
        }

        public bool IsItemInInventory(string itemName)
        {
            return _itemsList.Contains(itemName);
        }

        public void RemoveItemFromInventory(string itemName)
        {
            _itemsList.Remove(itemName);
            print($"{itemName} has been removed from the inventory");
            SaveInventory();
        }

        public void SaveInventory()
        {
            SaveSystemManager.Source.GetLoadedGame().SetInventory(_itemsList);
        }
    }
}
