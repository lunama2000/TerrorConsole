using System;

namespace UnityEngine
{
    [Serializable]
    public class SerializableInterface<T> where T : class
    {
        [SerializeField] private MonoBehaviour _object;

        private T _interfaceComponent;

        public T Value
        {
            get
            {
                if (_interfaceComponent != null) return _interfaceComponent;

                return _interfaceComponent = _object.GetComponent<T>();
            }
        }
    }
}
