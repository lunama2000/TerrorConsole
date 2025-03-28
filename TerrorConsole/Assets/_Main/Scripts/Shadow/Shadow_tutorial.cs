using UnityEngine;

namespace TerrorConsole
{
    public class Shadow_tutorial : MonoBehaviour
    {
        [SerializeField] Animator _aparicion;
        [SerializeField] private GameObject _shadow;
        [SerializeField] private Collider2D _door;

        private void FixedUpdate()
        {
            PrimerAparicion(); 
        }

        private void PrimerAparicion()
        {
            Debug.Log("puerta" + _door.enabled);
            if (_door.enabled == false)
            {
                Debug.Log("animación activada");
                _aparicion.Play("Primer aparición");
            }
        }

        private void Desaparicion()
        {
            _shadow.gameObject.SetActive(false);
        }
    }
}