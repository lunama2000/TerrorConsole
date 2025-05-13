using Unity.VisualScripting;
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
            TooltipsManager.Source.HideTooltip("Preview");

            if (!item)
                return;

            InventoryUIManager.Source.UpdateItemPreview(item);
            if (item.HasPreviewInInventory)
            {
                TooltipsManager.Source.ShowTooltip(KeyCode.Return, "Preview");
            }
        }

        public void OnItemClicked()
        {
            item.OnClickedInInventory();
        }
    }
}
