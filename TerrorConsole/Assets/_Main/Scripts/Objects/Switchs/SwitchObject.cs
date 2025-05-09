using UnityEngine;
using UnityEngine.Events;

namespace TerrorConsole
{
    public class SwitchObject : MonoBehaviour
    {
        [SerializeField] protected string _id;
        
        public UnityEvent<string> OnActivated;
        public UnityEvent<string> OnDeactivated;
        
        protected bool _state;

        protected virtual void AlternateState()
        {
            _state = !_state;
            if (_state)
            {
                OnActivated?.Invoke(_id);
                CameraSystemManager.Source.ShakeCamera();
            }
            else
            {
                OnDeactivated?.Invoke(_id);
            }
        }

        protected void On()
        {
            _state = true;
            OnActivated?.Invoke(_id);
        }

        protected void Off()
        {
            _state = false;
            OnDeactivated?.Invoke(_id);
        }
    }
}
