using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TerrorConsole
{
    public class PlayerEquipables : MonoBehaviour
    {
        [SerializeField] EquipableItem[] _equipables;

        private void Start()
        {
            Inventory.Source.OnInventoryUpdated += CheckIfItIsEquiped;
            CheckIfItIsEquiped();
        }

        private void OnDestroy()
        {
            Inventory.Source.OnInventoryUpdated -= CheckIfItIsEquiped;
        }

        private void CheckIfItIsEquiped()
        {
            foreach (EquipableItem item in _equipables)
            {
                item.gameObject.SetActive(Inventory.Source.IsItemInInventory(item.GetObjectInfo()));
            }
        }
    }
}
