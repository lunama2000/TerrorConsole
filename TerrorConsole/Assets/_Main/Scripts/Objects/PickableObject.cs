using UnityEngine;

namespace TerrorConsole
{
    public class PickableObject : MonoBehaviour
    {
        [SerializeField] private string _objectName;

        private void Start()
        {
            _objectName = _objectName == "" ? transform.name : _objectName;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PickedByPlayer();
            }
        }

        protected virtual void PickedByPlayer()
        {
            Inventory.Source.AddItemToInventory(_objectName);
            print($"Object {_objectName} have been collected by the Player");
            Destroy(gameObject);
        }
    }
}
