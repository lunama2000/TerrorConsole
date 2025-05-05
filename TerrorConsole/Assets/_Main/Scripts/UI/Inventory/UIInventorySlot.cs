using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class UIInventorySlot : MonoBehaviour, ISelectHandler
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private ItemInfo item;

        public void SetItem(ItemInfo newItem)
        {
            item = newItem;
            Initialize();
        }
        public void Initialize()
        {
            itemImage.sprite = item.ItemSprite;
            itemImage.color = Color.white;
        }

        public void OnSelect(BaseEventData eventData)
        {
            InventoryUIManager.Source.UpdateItemPreview(item);
        }
    }
}
