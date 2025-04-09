using UnityEngine;

namespace TerrorConsole
{
    public class PickableObject : MonoBehaviour
    {
        [SerializeField] private string _objectName;
        [SerializeField] private LevelEventsRecorder _eventRecorder;

        private void Start()
        {
            if (string.IsNullOrEmpty(_objectName))
            {
                Debug.LogError($"There is no Object Name for {name}, please set a unique name for this item");
                _objectName = _objectName == "" ? transform.name : _objectName;
            }

            if(Inventory.Source.IsItemInInventory(_objectName) || _eventRecorder.CheckEventState())
            {
                gameObject.SetActive(false);
            }
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
            LevelManager.Source.AddOrUpdateLevelEvent($"ITEM:{_objectName}",true);
            _eventRecorder.RegisterLevelEvent(true);
            Destroy(gameObject);
        }
    }
}
