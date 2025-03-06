using UnityEngine;

namespace TerrorConsole
{
    public class SwitchObject : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _switchInteractuableObject;
        protected ISwitchInteractuableSource _interactuableObject;

        protected bool _state;

        private void Start()
        {
            _interactuableObject = _switchInteractuableObject as ISwitchInteractuableSource;
        }

        protected virtual void On()
        {
            _state = true;
            _interactuableObject.SwitchOn();
        }
        protected virtual void Off()
        {
            _state = false;
            _interactuableObject.SwitchOff();
        }

        protected virtual void AlternateState()
        {
            _state = !_state;
            if (_state)
            {
                _interactuableObject.SwitchOn();
            }
            else
            {
                _interactuableObject.SwitchOff();
            }
        }
    }
}
