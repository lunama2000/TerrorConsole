using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class UIInventorySlot : MonoBehaviour, ISelectHandler
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private ItemInfo item;

        public void ResetItem()
        {
            item = null;
            itemImage.sprite = null;
            itemImage.color = new Color(1,1,1,0);
        }
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

        public void OnItemClicked()
        {
            item.OnClickedInInventory();
        }
    }
}
