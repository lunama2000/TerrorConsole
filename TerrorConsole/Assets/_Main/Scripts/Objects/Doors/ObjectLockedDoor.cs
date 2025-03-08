using UnityEngine;

namespace TerrorConsole
{
    public class ObjectLockedDoor : Door
    {
        [SerializeField] private string _neededItemName;

        protected override void PlayerCollisioned()
        {
            base.PlayerCollisioned();
            if (!_isLocked || !Inventory.Source.IsItemInInventory(_neededItemName))
            {
                return;
            }
            Inventory.Source.RemoveItemFromInventory(_neededItemName);
            UnlockDoor();
            OpenDoor();
        }
    }
}
