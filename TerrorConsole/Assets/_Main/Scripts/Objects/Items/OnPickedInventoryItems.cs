using UnityEngine;
using UnityEngine.Events;

namespace TerrorConsole
{
    public class OnPickedInventoryItems : MonoBehaviour
    {
        [SerializeField] private ItemInfo[] _itemsList;
        
        [SerializeField] private UnityEvent OnPickedItems;
        
        [SerializeField] private bool removeItemsAfterTrigger = false;

        private void Start()
        {
            CheckItems();

            Inventory.Source.OnInventoryUpdated += CheckItems;
        }

        private void OnDestroy()
        {
            Inventory.Source.OnInventoryUpdated -= CheckItems;
        }

        private void CheckItems()
        {
            for (int i = 0; i < _itemsList.Length; i++)
            {
                if (!Inventory.Source.IsItemInInventory(_itemsList[i])) return;
            }

            OnPickedItems?.Invoke();
            
            if (removeItemsAfterTrigger)
            {
                foreach (var item in _itemsList)
                {
                    Inventory.Source.RemoveItemFromInventory(item);
                }

                Inventory.Source.SaveInventory();
            }
        }
    }
}
