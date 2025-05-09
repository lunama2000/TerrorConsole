using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements.Experimental;

namespace TerrorConsole
{
    public class EquipableLantern : EquipableItem
    {
        [Header("COMPONENTS")]
        [SerializeField] private Light2D _lanternLight;

        [Header("PROPERTIES")]
        [SerializeField] private bool _isOn;

        [Header("CONFIGURATION")]
        public float _posRadius = 2f;
        public float _transitionDuration = 0.3f;

        private Tween _moveTween;
        private Tween _rotateTween;
        private Vector2 _direction;
        private Vector2 _lastInputDir = Vector2.zero;

        protected override void Start()
        {
            base.Start();
            _inputSource.OnActivateButton2 += SwitchLight;
            TooltipsManager.Source.ShowTooltip(InputActionsInGame.Button2, "Lantern");
            TurnLight(false);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _inputSource.OnActivateButton2 -= SwitchLight;
        }

        private void SwitchLight()
        {
            if (_freezeInput) return;
            _isOn = !_isOn;
            _direction = _inputSource.LastLookDirection;
            LookInmetiateToDir();
            _lanternLight.enabled = _isOn;
        }

        private void TurnLight(bool turnOn)
        {
            _isOn = turnOn;
            _direction = _inputSource.LastLookDirection;
            LookInmetiateToDir();
            _lanternLight.enabled = _isOn;
        }

        private void Update()
        {
            if (!_isOn || _freezeInput) return;

            _direction = InputManager.Source.LastLookDirection;
            LookToDir();
        }

        private void LookToDir()
        {
            if (_lastInputDir == _direction || _direction == Vector2.zero)
            {
                return;
            }
            _lastInputDir = _direction;
            Vector2 localOffset = _direction * _posRadius;

            _moveTween?.Kill();
            _rotateTween?.Kill();

            _moveTween = transform.DOLocalMove(localOffset, _transitionDuration).SetEase(Ease.InOutSine);

            float angle = Mathf.Atan2(localOffset.y, localOffset.x) * Mathf.Rad2Deg - 90f;
            _rotateTween = transform.DOLocalRotate(new Vector3(0f, 0f, angle), _transitionDuration).SetEase(Ease.InOutSine);
        }

        private void LookInmetiateToDir()
        {
            Vector2 localOffset = _direction * _posRadius;

            transform.localPosition = localOffset;

            float angle = Mathf.Atan2(localOffset.y, localOffset.x) * Mathf.Rad2Deg - 90f;
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }

    }
}
