using UnityEngine;

namespace TerrorConsole
{
    public class PickableObject : MonoBehaviour
    {
        [SerializeField] private string _objectName;

        private void Start()
        {
            if (string.IsNullOrEmpty(_objectName))
            {
                Debug.LogError($"There is no Object Name for {name}, please set a unique name for this item");
                _objectName = _objectName == "" ? transform.name : _objectName;
            }

            gameObject.SetActive(!Inventory.Source.IsItemInInventory( _objectName));
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
