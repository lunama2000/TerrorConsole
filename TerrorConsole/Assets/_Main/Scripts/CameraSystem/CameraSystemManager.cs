using UnityEngine;

namespace TerrorConsole
{
    public class CameraSystemManager : Singleton<ICameraSystemSource>, ICameraSystemSource
    {
        private CameraZone _activeZone;

        [SerializeField]
        private CameraZone _freeCamera;

        public void ActivateCameraZone(CameraZone zoneToActivate)
        {
            if (_activeZone != null)
            {
                _activeZone.DeactivateZone();
            }
            zoneToActivate.ActivateZone();
            _activeZone = zoneToActivate;
        }

        public void DeactvateCameraZone(CameraZone zoneToDeactivate)
        {
            if (_activeZone == zoneToDeactivate)
            {
                ActivateCameraZone(_freeCamera);
            }
        }

        public void FreeCamera()
        {
            ActivateCameraZone(_freeCamera);
        }
    }
}
