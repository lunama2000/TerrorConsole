using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class BossFightPhaseOne : MonoBehaviour
    {
        [Header("Shadow Health")]
        [SerializeField]private GameObject _shadowHealthCanvas;
        [SerializeField]private int _maxHealthShadow = 0;
        [SerializeField]private Slider _healthSlider;
        [SerializeField]private TMP_Text _healthText;
        
        [Header("Levers Locations")]
        [SerializeField] private List<GameObject> _leverLocations;
        private List<GameObject> _activeLevers = new List<GameObject>();
        
        [Header("Events")]
        [SerializeField]public UnityEvent _onTimerEnd;
        [SerializeField]public UnityEvent _onTimerStart;
        
        [Header("Timer")]
        [SerializeField]private GameObject _timerCanvas;
        [SerializeField]private TMP_Text _timerText;
        [SerializeField] private float _timerDuration = 30f;
        
        private bool _timerActive;
        
        [Header("Lever Controller")]
        OrderLeverController _orderLeverController;
        
        public void BeginBossFightPhaseOne()
        {
            _shadowHealthCanvas.gameObject.SetActive(true);
            _timerCanvas.gameObject.SetActive(true);
            _healthSlider.maxValue = 100;
            _healthSlider.value = _maxHealthShadow;
            
            ActivateRandomLevers(3);
            StartTimer();
        }
        
        public void ReceiveDamage(int damage)
        {
            DOTween.To(() => _healthSlider.value, UpdateHealth, _healthSlider.value - damage, 0.5f).SetEase(Ease.InFlash);
        }
        
        private void UpdateHealth(float health)
        {
            _healthSlider.value = health;
        }
        
        private void StartTimer()
        {
            _timerActive = true;
            _timerDuration -= Time.deltaTime;
            _timerText.text = _timerDuration.ToString();
            
            _orderLeverController = FindObjectOfType<OrderLeverController>();
            _orderLeverController.SetAllowAnyOrder(true);

            
            if (_timerDuration <= 0)
            {
                _onTimerEnd?.Invoke();
            }
            
        }
        
        private void ActivateRandomLevers(int amount)
        {
            foreach (var lever in _activeLevers)
            {
                lever.SetActive(false);
            }
            _activeLevers.Clear();
            
            List<GameObject> shuffled = new List<GameObject>(_leverLocations);
            for (int i = 0; i < shuffled.Count; i++)
            {
                GameObject temp = shuffled[i];
                int randomIndex = Random.Range(i, shuffled.Count);
                shuffled[i] = shuffled[randomIndex];
                shuffled[randomIndex] = temp;
            }
            
            for (int i = 0; i < Mathf.Min(amount, shuffled.Count); i++)
            {
                shuffled[i].SetActive(true);
                _activeLevers.Add(shuffled[i]);
            }
        }

        public void EndBossFightPhaseOne()
        {
            _timerActive = false;
            
        }
    }
}
