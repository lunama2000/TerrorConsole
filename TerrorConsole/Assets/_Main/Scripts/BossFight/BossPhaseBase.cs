using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TerrorConsole
{
    public abstract class BossPhaseBase : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] protected GameObject _shadowHealthCanvas;
        [SerializeField] protected Slider _healthSlider;
        [SerializeField] protected TMP_Text _healthText;
        [SerializeField] protected int _maxHealthShadow = 100;

        [Header("Timer")]
        [SerializeField] protected GameObject _timerCanvas;
        [SerializeField] protected TMP_Text _timerText;
        [SerializeField] protected float _timerDuration = 30f;
        protected float _timerRemaining;
        protected bool _timerActive;

        [Header("Events")]
        [SerializeField] protected UnityEvent _onTimerEnd;
        [SerializeField] protected UnityEvent _onTimerStart;

        [SerializeField] private string _damageSFXKey;
        
        protected bool _useHealthUI = false;
        protected bool _useTimer = false;
        protected bool _resetHealthOnStart = false;

        protected virtual void Update()
        {
            CountingTimer();
        }

       
        protected virtual void StartPhase(bool useHealthUI = false, bool resetHealth = false, bool useTimer = false)
        {
            _useHealthUI = useHealthUI;
            _useTimer = useTimer;
            _resetHealthOnStart = resetHealth;

            if (_useHealthUI && _shadowHealthCanvas != null)
            {
                _shadowHealthCanvas.SetActive(true);

                if (_resetHealthOnStart && _healthSlider != null)
                {
                    _healthSlider.maxValue = _maxHealthShadow;
                    _healthSlider.value = _maxHealthShadow;
                }
            }

            if (_useTimer && _timerCanvas != null)
            {
                _timerCanvas.SetActive(true);
                StartTimer();
            }
            else
            {
                _timerCanvas.SetActive(false);
                StopTimer();
            }
        }
        
        protected void StartTimer()
        {
            _timerRemaining = _timerDuration;
            _timerActive = true;
            _onTimerStart?.Invoke();
        }

        protected void CountingTimer()
        {
            if (_timerActive)
            {
                _timerRemaining -= Time.deltaTime;

                if (_timerText != null)
                    _timerText.text = FormatTime(_timerRemaining);

                if (_timerRemaining <= 0f)
                {
                    _timerActive = false;
                    _onTimerEnd?.Invoke();
                    HideTimer();
                }
            }
        }
        
        public void ResetTimer(float newDuration)
        {
            _timerDuration = newDuration;
            _timerRemaining = newDuration;
            _timerActive = true;

            if (_timerCanvas != null)
                _timerCanvas.SetActive(true);

            _onTimerStart?.Invoke();
        }

       
        protected void StopTimer()
        {
            _timerActive = false;
        }
        
        protected void HideHealthUI()
        {
            if (_shadowHealthCanvas != null)
                _shadowHealthCanvas.SetActive(false);
        }

        protected void HideTimer()
        {
            if (_timerCanvas != null)
            {
                _timerCanvas.SetActive(false);
            }
        }
        
        protected void ReceiveDamage(int damage)
        {
            if (_healthSlider == null)
                return;

            float targetHealth = Mathf.Max(0f, _healthSlider.value - damage);

            DOTween.To(() => _healthSlider.value, UpdateHealth, targetHealth, 0.5f)
                .SetEase(Ease.InFlash);

            if (!string.IsNullOrEmpty(_damageSFXKey))
            {
                AudioManager.Source.PlaySFX(_damageSFXKey);
            }

            if (targetHealth <= 0f)
            {
                OnBossDefeated();
            }
        }
        
        private void UpdateHealth(float value)
        {
            _healthSlider.value = value;
        }
        
        protected virtual void OnBossDefeated()
        {
            HideHealthUI();
            Debug.Log("Boss defeated.");
        }
        
        protected string FormatTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            return $"{minutes:00}:{seconds:00}";
        }
    }
}
