using UnityEngine;

namespace TerrorConsole
{
    public class Credits : MonoBehaviour
    {
        public float scrollSpeed = 10f;
        public RectTransform rectTransform;
        private float startPositionY;


        void Start()
        {
            startPositionY = rectTransform.anchoredPosition.y;
            rectTransform = GetComponent<RectTransform>();
        }

        void Update ()
        {
            rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

            if (rectTransform.anchoredPosition.y > Screen.height)
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -Screen.height);
            }
        }

    }
}
