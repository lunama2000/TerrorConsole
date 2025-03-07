using UnityEngine;

namespace TerrorConsole
{
    public class Loop : MonoBehaviour
    {
        public Transform PuntoB;

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Creditos"))
            {
                collider.transform.position = PuntoB.position;
            }
        }
    }
}
