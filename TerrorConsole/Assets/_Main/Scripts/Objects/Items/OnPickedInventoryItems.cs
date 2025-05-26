using UnityEngine;
using UnityEngine.Events;

namespace TerrorConsole
{
    public class OnPickedInventoryItems : MonoBehaviour
    {
        [SerializeField] private ItemInfo[] _itemsList;
        
        public UnityEvent OnPickedItems;

        void Start()
        {
            CheckItems();

            Inventory.Source.OnInventoryUpdated += CheckItems;
        }

        void OnDestroy()
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
        }
    }
}
