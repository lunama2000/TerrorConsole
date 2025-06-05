using System;
using UnityEngine;

namespace TerrorConsole
{
    public class PickableObject : MonoBehaviour
    {
        [SerializeField] private String _pickableSFXKey;
        [SerializeField] protected ItemInfo _itemInfo;
        [SerializeField] private LevelEventsRecorder _eventRecorder;
        [SerializeField] private bool _isBossLevelItem = false;
        [SerializeField] private bool _disableEventRecording = false;

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

            if (Inventory.Source.IsItemInInventory(_itemInfo))
            {
                Destroy(gameObject);
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

            if (!_disableEventRecording && _eventRecorder != null && !_isBossLevelItem)
            {
                Debug.Log("Evento registrado");
                _eventRecorder.RegisterLevelEvent(true);
            }
            
            AudioManager.Source.PlaySFX(_pickableSFXKey);
            Destroy(gameObject);
        }
    }
}
