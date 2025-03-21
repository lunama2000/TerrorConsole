using UnityEngine;

namespace TerrorConsole
{
    public interface ICameraSystemSource
    {
        void ActivateCameraZone(CameraZone xoneToAxtivate);
        void DeactvateCameraZone(CameraZone zoneToDeactivate);
        void FreeCamera();
    }
}
