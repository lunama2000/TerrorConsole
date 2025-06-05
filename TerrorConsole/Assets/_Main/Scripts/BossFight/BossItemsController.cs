using System;
using UnityEngine;

namespace TerrorConsole
{
    public class BossItemsController : MonoBehaviour
    {
        [Header("Items de Boss que deben ser reiniciados")]
        [SerializeField] private ItemInfo[] bossItems;

        [Header("Event Keys asociados a esos objetos (mismo orden)")]
        [SerializeField] private string[] eventKeys;
        
        [SerializeField] private bool _resetOnStart = true;


        private void Start()
        {
            if (_resetOnStart)
            {
                if (LevelManager.Source.GetCurrentLevelState() == LevelState.BossPhase)
                {
                    LevelManager.Source.ChangeLevelState(LevelState.Play);
                }
                ResetBossItems();
            }
        }

        public void ResetBossItems()
        {
            foreach (var item in bossItems)
            {
                Inventory.Source.RemoveItemFromInventory(item);
            }

            foreach (var eventKey in eventKeys)
            {
                LevelManager.Source.AddOrUpdateLevelEvent(eventKey, false);
            }

            Inventory.Source.SaveInventory();

            Debug.Log("<color=yellow>Boss items y eventos han sido reseteados.</color>");
        }
    }
}
