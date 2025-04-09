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

        public SaveLevelData(int levelNumber)
        {
            _levelNumber = levelNumber;
            _eventsInLevel = new List<LevelEvent>();
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

        public void UpdateLevelEvent(string eventName, bool newEventState)
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
                Debug.Log($"There is no event with key {eventName}");
                return false;
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
}
