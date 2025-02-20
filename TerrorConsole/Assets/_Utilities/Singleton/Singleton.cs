namespace UnityEngine
{
    public class Singleton<I> : MonoBehaviour where I : class
    {
        public static I Source { get; private set; }
        
        protected virtual void Awake()
        {
            if (Source != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            
            Source = this as I;
            DontDestroyOnLoad(gameObject);
        }
    }
}
