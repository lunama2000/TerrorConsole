using DG.Tweening;
using UnityEngine;

namespace TerrorConsole
{
    public class CameraSystemManager : Singleton<ICameraSystemSource>, ICameraSystemSource
    {
        private CameraZone _activeZone;

        [SerializeField]
        private CameraZone _freeCamera;
        
        [SerializeField] private float _shakeDuration = 1f;
        [SerializeField] private float _shakeStrength = 2f;
        
        public void ActivateCameraZone(CameraZone zoneToActivate)
        {
            if (_activeZone != null)
            {
                _activeZone.DeactivateZone();
            }
            zoneToActivate.ActivateZone();
            _activeZone = zoneToActivate;
        }

        public void DeactivateCameraZone(CameraZone zoneToDeactivate)
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
        
        public void ShakeCamera()
        {
            if (_activeZone != null)
            {
                _activeZone.ShakeCamera(_shakeDuration, _shakeStrength);
            }
            else
            {
                Debug.LogWarning("There is no active zone to be shaked.");
            }
        }
        
    }
}
