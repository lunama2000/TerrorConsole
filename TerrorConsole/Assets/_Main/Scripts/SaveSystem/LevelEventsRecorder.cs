using UnityEditor;
using UnityEngine;

namespace TerrorConsole
{
    public class LevelEventsRecorder : MonoBehaviour
    {
        [SerializeField] string _uniqueEventKey;

        [SerializeField] private bool _deactivateIfLevelEvent;

        private void Start()
        {
            if (string.IsNullOrEmpty(_uniqueEventKey))
            {
                Debug.LogError($"There is no Event Key for {name}, please set a unique Event Key for this item");
                _uniqueEventKey = name;
            }
            else if(_deactivateIfLevelEvent)
            {
                gameObject.SetActive(CheckEventState());
            }
        }

        public bool CheckEventState()
        {
            return LevelManager.Source.GetEventState(_uniqueEventKey);
        }

        public void RegisterLevelEvent(bool eventState)
        {
            LevelManager.Source.AddOrUpdateLevelEvent(_uniqueEventKey, eventState);
        }
    }
}
