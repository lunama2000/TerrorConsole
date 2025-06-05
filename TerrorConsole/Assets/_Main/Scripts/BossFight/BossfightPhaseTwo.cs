using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class BossfightPhaseTwo : BossPhaseBase
    {
        [SerializeField] private BossfightPhaseThree _bossfightPhaseThree;
        [Header("Objects Locations")]
        [SerializeField] private List<GameObject> _ObjectsLocations;
        
        
        
        public void BeginBossFightPhaseTwo()
        {
            this.enabled = true;
          StartPhase(true, false,true);
          
          ActivateObjectsLocations();
        }


        private void ActivateObjectsLocations()
        {
            foreach (var objects in _ObjectsLocations)
            {
                objects.SetActive(true);
            }
        }

        public void DamageBoss(int damage)
        {
            ReceiveDamage(damage);
        }
        
        public void EndBossFightPhaseTwo()
        {
            this.enabled = false;
            _bossfightPhaseThree.BeginBossFightPhaseThree();
        }
    }
}
