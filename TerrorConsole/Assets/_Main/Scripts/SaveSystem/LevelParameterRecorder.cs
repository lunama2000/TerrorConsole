using UnityEngine;

namespace TerrorConsole
{
    public class LevelParameterRecorder : MonoBehaviour
    {
        [SerializeField] string _uniqueParameterKey;

        public int GetParameterValue()
        {
            return LevelManager.Source.GetLevelParameterValue(_uniqueParameterKey);
        }

        public void RegisterLevelParameter(int parameterValue)
        {
            LevelManager.Source.AddOrUpdateLevelParameter(_uniqueParameterKey, parameterValue);
        }
    }
}
