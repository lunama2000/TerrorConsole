using UnityEngine;

namespace TerrorConsole
{
    public class SwitchCameraActivation : CameraActivation, ISwitchInteractable
    {
        public void SwitchOn()
        {
            LeverActivated();
        }

        public void SwitchOff()
        {
            
        }
    }
}
