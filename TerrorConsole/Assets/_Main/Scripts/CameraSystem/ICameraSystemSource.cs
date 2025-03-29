using UnityEngine;

namespace TerrorConsole
{
    public interface ICameraSystemSource
    {
        void ActivateCameraZone(CameraZone zoneToActivate);
        void DeactvateCameraZone(CameraZone zoneToDeactivate);
        void FreeCamera();
        void ShakeCamera();
    }
}
