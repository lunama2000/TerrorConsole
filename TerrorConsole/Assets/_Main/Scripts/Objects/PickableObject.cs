using System;
using UnityEngine;

namespace TerrorConsole
{
    public class PickableObject : MonoBehaviour
    {
        [SerializeField] private String _pickableSFXKey;
        [SerializeField] protected ItemInfo _itemInfo;
        [SerializeField] private LevelEventsRecorder _eventRecorder;

        private void Start()
        {
            if (!_itemInfo)
            {
                Debug.LogError($"There is no Item Info for {name}, please set the corresponding scriptable Object");
            }

            if (_eventRecorder.CheckEventState())
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
            Inventory.Source.AddItemToInventory(_itemInfo);
            _eventRecorder.RegisterLevelEvent(true);
            AudioManager.Source.PlaySFX(_pickableSFXKey);
            Destroy(gameObject);
        }
    }
}
