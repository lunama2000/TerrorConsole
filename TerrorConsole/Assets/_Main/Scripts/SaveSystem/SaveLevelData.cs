using System;
using System.Collections.Generic;
using UnityEngine;

namespace TerrorConsole
{
    [Serializable]
    public class SaveLevelData
    {
        [SerializeField] private int _levelNumber = 0;
        [SerializeField] private List<LevelEvent> _eventsInLevel;
        [SerializeField] private List<LevelParameter> _parametersInLevel;

        public SaveLevelData(int levelNumber)
        {
            _levelNumber = levelNumber;
            _eventsInLevel = new List<LevelEvent>();
            _parametersInLevel = new List<LevelParameter>();
        }

        public void SetLevelNumber(int newLevelNumber)
        {
            _levelNumber = newLevelNumber;
        }

        public int GetLevelNumber()
        {
            return _levelNumber;
        }

        #region LevelEvents
        public void AddEventoToLevel(string eventName, bool eventState)
        {
            if (CheckIfEventExist(eventName))
            {
                Debug.Log($"Event {eventName} already exists in this level");
            }
            else
            {
                _eventsInLevel.Add(new LevelEvent(eventName, eventState));
            }    
        }

        private void UpdateLevelEvent(string eventName, bool newEventState)
        {
            if (CheckIfEventExist(eventName))
            {
                foreach (LevelEvent levelEvent in _eventsInLevel)
                {
                    if (levelEvent.EventName == eventName)
                    {
                        levelEvent.EventState = newEventState;
                        break;
                    }
                }
            }
            else
            {
                Debug.Log($"There is no event with key {eventName}");

            }
        }

        public void AddOrUpdateLevelEvent(string eventName, bool eventState)
        {
            if (CheckIfEventExist(eventName))
            {
                UpdateLevelEvent(eventName, eventState);
            }
            else
            {
                AddEventoToLevel(eventName, eventState);

            }
        }

        public bool CheckIfEventExist(string eventName)
        {
            foreach (LevelEvent levelEvent in _eventsInLevel)
            {
                if (levelEvent.EventName == eventName)
                {
                    return true;
                }
            }
            return false;
        }

        public bool GetEventState(string eventName)
        {
            if (CheckIfEventExist(eventName))
            {
                foreach (LevelEvent levelEvent in _eventsInLevel)
                {
                    if (levelEvent.EventName == eventName)
                    {
                        return levelEvent.EventState;
                    }
                }
                return false;
            }
            else
            {
                //Debug.Log($"There is no event with key {eventName}");
                return false;
            }
        }
        #endregion

        #region LevelParameters
        public void AddParameterToLevel(string parameterName, int parameterValue)
        {
            if (CheckIfEventExist(parameterName))
            {
                Debug.Log($"Event {parameterName} already exists in this level");
            }
            else
            {
                _parametersInLevel.Add(new LevelParameter(parameterName, parameterValue));
            }
        }

        private void UpdateLevelParameter(string parameterName, int newParameterValue)
        {
            if (CheckIfParameterExist(parameterName))
            {
                foreach (LevelParameter levelParameter in _parametersInLevel)
                {
                    if (levelParameter.ParameterName == parameterName)
                    {
                        levelParameter.ParameterValue = newParameterValue;
                        break;
                    }
                }
            }
            else
            {
                Debug.Log($"There is no parameter with key {parameterName}");

            }
        }

        public void AddOrUpdateLevelParameter(string parameterName, int parameterValue)
        {
            if (CheckIfParameterExist(parameterName))
            {
                UpdateLevelParameter(parameterName, parameterValue);
            }
            else
            {
                AddParameterToLevel(parameterName, parameterValue);

            }
        }

        public bool CheckIfParameterExist(string paramterName)
        {
            foreach (LevelParameter levelParameter in _parametersInLevel)
            {
                if (levelParameter.ParameterName == paramterName)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetParameterValue(string parameterName)
        {
            if (CheckIfParameterExist(parameterName))
            {
                foreach (LevelParameter levelParameter in _parametersInLevel)
                {
                    if (levelParameter.ParameterName == parameterName)
                    {
                        return levelParameter.ParameterValue;
                    }
                }
                return -1;
            }
            else
            {
                //Debug.Log($"There is no event with key {eventName}");
                return -1;
            }
        }
        #endregion
    }

    [Serializable]
    public class LevelEvent
    {
        public string EventName;
        public bool EventState;

        public LevelEvent(string newEventName, bool newEventState)
        {
            EventName = newEventName;
            EventState = newEventState;
        }
    }

    [Serializable]
    public class LevelParameter
    {
        public string ParameterName;
        public int ParameterValue;

        public LevelParameter(string newParameterName, int newParameterValue)
        {
            ParameterName = newParameterName;
            ParameterValue = newParameterValue;
        }
    }
}
