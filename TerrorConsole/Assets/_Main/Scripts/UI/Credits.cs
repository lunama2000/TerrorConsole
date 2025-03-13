using UnityEngine;

namespace TerrorConsole
{
    public class Credits : MonoBehaviour
    {
        public float scrollSpeed = 10f;
        public RectTransform rectTransform;
        private float startPositionY; // Guardamos la posición inicial en Y

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            startPositionY = rectTransform.anchoredPosition.y; // Guardamos la posición inicial
        }

        void Update()
        {
            // Movemos el cuadro de texto hacia arriba
            rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

            // Si el cuadro de texto sale de la pantalla por la parte superior
            if (rectTransform.anchoredPosition.y > Screen.height)
            {
                // Lo reposicionamos al inicio (en la parte inferior de la pantalla)
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, startPositionY);
            }
        }
    }
}
