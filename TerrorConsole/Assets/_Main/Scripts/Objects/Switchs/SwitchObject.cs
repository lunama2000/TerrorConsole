using UnityEngine;

namespace TerrorConsole
{
    public class SwitchObject : MonoBehaviour
    {
        [SerializeField] private SerializableInterface<ISwitchInteractable> _switchLockedDoor;

        protected bool _state;

        protected virtual void On()
        {
            _state = true;
            _switchLockedDoor.Value.SwitchOn();
        }

        protected virtual void Off()
        {
            _state = false;
            _switchLockedDoor.Value.SwitchOff();
        }

        protected virtual void AlternateState()
        {
            _state = !_state;
            if (_state)
            {
                    _switchLockedDoor.Value.SwitchOn();
            }
            else
            {
                _switchLockedDoor.Value.SwitchOff();
            }
        }
    }
}
