using Cinemachine;
using UnityEngine;

namespace TerrorConsole
{
    [RequireComponent (typeof (PolygonCollider2D))]
    public class CameraZone : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera _virtualCamera;

        [SerializeField]
        [Tooltip ("True if this zone is not delimeted to a specific room or section")]
        private bool _isFreeCamera = false;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(_isFreeCamera || !collider.CompareTag("Player")) return;

            if (_virtualCamera.Follow == null)
            {
                Debug.LogError($"Please add the player to the 'Follow' section in the virtual camera: {_virtualCamera.name}");
            }
            CameraSystemManager.Source.ActivateCameraZone(this);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                CameraSystemManager.Source.DeactvateCameraZone(this);
            }
        }

        public void ActivateZone()
        {
            _virtualCamera.gameObject.SetActive(true);
        }

        public void DeactivateZone()
        {
            _virtualCamera.gameObject.SetActive(false);
        }
    }
}
