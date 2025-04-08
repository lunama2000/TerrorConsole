using UnityEditor;
using UnityEngine;

namespace TerrorConsole
{
    public class LevelEventsRecorder : MonoBehaviour
    {
        [SerializeField] string _uniqueEventKey;

        private void Start()
        {
            if (string.IsNullOrEmpty(_uniqueEventKey))
            {
                Debug.LogError($"There is no Event Key for {name}, please set a unique Event Key for this item");
                _uniqueEventKey = name;
            }
        }

        public bool CheckEventState()
        {
            return LevelManager.Source.GetSaveLevelData().GetEventState(_uniqueEventKey);
        }
        public void RegisterLevelEvent(bool eventState)
        {
            LevelManager.Source.GetSaveLevelData().AddOrUpdateLevelEvent(_uniqueEventKey, eventState);
        }
    }
}
