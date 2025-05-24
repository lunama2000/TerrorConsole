using UnityEngine;

namespace TerrorConsole
{
    [CreateAssetMenu(fileName = "GemInfo", menuName = "ScriptableObjects/GemInfo")]
    public class GemInfo : ItemInfo
    {
        [SerializeField] private GemColors _gemColor;

        public GemColors GemColor => _gemColor;
        
    }
}
