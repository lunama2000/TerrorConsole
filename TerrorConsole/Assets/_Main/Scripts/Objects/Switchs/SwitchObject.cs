using UnityEngine;

namespace TerrorConsole
{
    public class SwitchObject : MonoBehaviour
    {
        [SerializeField] protected SwitchLockedDoor _switchLockedDoor;

        protected bool _state;

        protected virtual void On()
        {
            _state = true;
            _switchLockedDoor.SwitchOn();
        }

        protected virtual void Off()
        {
            _state = false;
            _switchLockedDoor.SwitchOff();
        }

        protected virtual void AlternateState()
        {
            _state = !_state;
            if (_state)
            {
                _switchLockedDoor.SwitchOn();
            }
            else
            {
                _switchLockedDoor.SwitchOff();
            }
        }
    }
}
