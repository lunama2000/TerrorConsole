using UnityEngine;

namespace TerrorConsole
{
    public class SwitchLockedDoor : Door, ISwitchInteractable
    {
        public void SwitchOff()
        {
            LockDoor();
            CloseDoor();
        }

        public void SwitchOn()
        {
            UnlockDoor();
            OpenDoor();
        }
    }
}
