using UnityEngine;

namespace TerrorConsole
{
    public class OrderSwitchLockedDoor : Door
    {
        public void UnlockOrderPuzzleDoor()
        {
            UnlockDoor();
            OpenDoor();
            Debug.Log("Door is unlocked");
        }
    }
}
