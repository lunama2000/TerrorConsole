using System;
using UnityEngine;
using UnityEngine.Events;

namespace TerrorConsole
{
    public class SwitchObject : MonoBehaviour
    {
        [SerializeField] protected string _id;
        [SerializeField] protected SerializableInterface<ISwitchInteractable> _switchLockedDoor;
        
        public UnityEvent<string> OnActivated;
        public UnityEvent<string> OnDeactivated;
        
        protected bool _state;

        protected virtual void On()
        {
            _state = true;
            _switchLockedDoor.Value.SwitchOn();
            OnActivated?.Invoke(_id);
        }

        protected virtual void Off()
        {
            _state = false;
            _switchLockedDoor.Value.SwitchOff();
            OnDeactivated?.Invoke(_id);
        }

        protected virtual void AlternateState()
        {
            _state = !_state;
            if (_state)
            {
                    _switchLockedDoor.Value.SwitchOn();
                    CameraSystemManager.Source.ShakeCamera();
            }
            else
            {
                _switchLockedDoor.Value.SwitchOff();
            }
        }
    }
}
