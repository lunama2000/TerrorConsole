using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class BossFightPhaseOne : BossPhaseBase
    {
        [SerializeField] private BossfightPhaseTwo _bossfightPhaseTwo;
        [Header("Levers Locations")]
        [SerializeField] private List<GameObject> _leverLocations;
        private List<GameObject> _activeLevers = new List<GameObject>();

        [Header("Lever Controller")]
        private OrderLeverController _orderLeverController;

        public void BeginBossFightPhaseOne()
        {
            StartPhase(useHealthUI: true, resetHealth: true, useTimer: true);

            _orderLeverController = FindObjectOfType<OrderLeverController>();

            ActivateRandomLevers(3);
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
        
        public void DamageBoss(int damage)
        {
            ReceiveDamage(damage);
        }
        
        public void EndBossFightPhaseOne()
        {
            StopTimer();
            _bossfightPhaseTwo.BeginBossFightPhaseTwo();
            this.enabled = false;
            
        }
    }
}
