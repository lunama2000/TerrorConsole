using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TerrorConsole
{
    public class CameraActivation : MonoBehaviour, ICameraShake
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _shakeDuration = 0.5f;
        [SerializeField] private float _shakeStrength = 0.5f;


        protected virtual void LeverActivated()
        {
            CameraShake();
        }

        public void CameraShake()
        {
            _camera.DOShakePosition(_shakeDuration, _shakeStrength, 10, 60f, true);
            Debug.Log("Activated Camera Shake");
        }
    }
}
