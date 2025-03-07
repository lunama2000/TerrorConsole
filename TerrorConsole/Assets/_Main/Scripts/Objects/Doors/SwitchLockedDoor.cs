using UnityEngine;

namespace TerrorConsole
{
    public class SwitchLockedDoor : Door, ISwitchInteractuableSource
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
