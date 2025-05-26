using UnityEngine;

namespace TerrorConsole
{
    public class EquipableItem : MonoBehaviour
    {
        [SerializeField] protected bool _freezeInput;
        [SerializeField] private ItemInfo _itemInfo;

        protected IInputSource _inputSource;

        public ItemInfo GetObjectInfo()
        {
            return _itemInfo;
        }

        protected virtual void Start()
        {
            _inputSource = InputManager.Source;
            LevelManager.Source.OnLevelStateChange += OnLevelStateChange;
            if (!_itemInfo)
            {
                Debug.LogError($"There is no Item Info for {name}, please set the Item Info of the corresponding Pickable Item for this equipable");
            }
            gameObject.SetActive(Inventory.Source.IsItemInInventory(_itemInfo));
        }

        protected virtual void OnDestroy()
        {
            LevelManager.Source.OnLevelStateChange -= OnLevelStateChange;
        }

        protected virtual void OnLevelStateChange(LevelState newState)
        {
            switch (newState)
            {
                case LevelState.Play:
                    ResumeInput();
                    break;
                default:
                    StopInput();
                    break;
            }
        }

        protected virtual void StopInput()
        {
            _freezeInput = true;
        }

        protected virtual void ResumeInput()
        {
            _freezeInput = false;
        }
    }
}
