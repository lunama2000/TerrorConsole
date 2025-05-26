using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class BossfightPhaseTwo : MonoBehaviour
    {
        
        [SerializeField]private Slider _healthSlider;
        
        [Header("Objects Locations")]
        [SerializeField] private List<GameObject> _ObjectsLocations;
        
        
        public void BeginBossFightPhaseTwo()
        {
              
        }
    }
}
