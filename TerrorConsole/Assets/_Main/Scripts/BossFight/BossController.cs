using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class BossController : MonoBehaviour
    {
        [Header("Shadow Health")]
        [SerializeField]private Canvas _shadowHealthCanvas;
        [SerializeField]private int _healthShadow;
        [SerializeField]private int _damage;
        [SerializeField]private Slider _healthSlider;
        [SerializeField]private Text _healthText;
        
        [Header("Timer")]
        [SerializeField]private Text _timerText;
        [SerializeField] private float _timerDuration = 20f;
        
        private bool _timerActive;
        
        [Header("Events")]
        [SerializeField]public UnityEvent _onTimerEnd;
        [SerializeField]public UnityEvent _onTimerStart;
        
        [Header("Objects Location")]
        [SerializeField] private List<GameObject> _shadowLocations;
        [SerializeField] private List<GameObject> _leverLocations;
        
        [Header("Shadow Configuration")]
        [SerializeField] private float _shadowSpeed;

        public void StartTimer()
        {
            _timerActive = true;
            _timerText.text = _timerDuration.ToString();
        }

        public void StopTimer()
        {
            _timerActive = false;
        }
        
        public void ResetTimer()
        {
            
        }
        
        public void UpdateHealth()
        {
            _healthSlider.value = _healthShadow;
        }

        public void ReceiveDamage(int damage)
        {
            _healthShadow -= damage;
        }
        
        
        
    }
}
