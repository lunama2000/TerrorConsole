using UnityEngine;

namespace TerrorConsole
{
    public class PickableObject : MonoBehaviour
    {
        [SerializeField] private string _objectName;

        private void Start()
        {
            _objectName = _objectName == "" ? transform.name : _objectName;
            //gameObject.SetActive(!Inventory.Source.IsItemInInventory(_objectName)); The object wont appear if it is alreay in inventory -- Commented this since idk if this is necessary
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
            Destroy(gameObject);
        }
    }
}
