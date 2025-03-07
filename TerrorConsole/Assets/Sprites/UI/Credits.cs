using UnityEngine;

namespace TerrorConsole
{
    public class Credits : MonoBehaviour
    {
        public float scrollSpeed = 10f;
        public RectTransform rectTransform;

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        void Update ()
        {
            rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
        }
            
    }
}
