using UnityEngine;

namespace TerrorConsole
{
    public interface ICameraSystemSource
    {
        void ActivateCameraZone(CameraZone zoneToActivate);
        void DeactivateCameraZone(CameraZone zoneToDeactivate);
        void FreeCamera();
        void ShakeCamera();
        void ShakeCamera(float strength);
    }
}
