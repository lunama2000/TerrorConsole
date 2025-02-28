namespace UnityEngine
{
    public class Singleton<I> : MonoBehaviour where I : class
    {
        public static I Source { get; private set; }

        [Header("SINGLETON")]
        [SerializeField] private bool isPersistent = true;

        protected virtual void Awake()
        {
            if (Source != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            
            Source = this as I;
            if (isPersistent)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
