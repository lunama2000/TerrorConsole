using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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
        
        private CinemachineBasicMultiChannelPerlin _noise;
        private void Awake()
        {
            _noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        
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

        public void ShakeCamera(float duration, float intensity)
        {
            if (_noise == null)
            {
                Debug.LogWarning("not found the noise component to the 'Noise' section in the virtual camera.");
                return;
            }
            _noise.m_AmplitudeGain = intensity;
            
            DOVirtual.Float(intensity, 0, duration, value =>
            {
                _noise.m_AmplitudeGain = value;
            });
        }
        
    }
}
