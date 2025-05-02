using UnityEngine;

namespace TerrorConsole
{
    public interface IInventoryUISource
    {
        void UpdateItemPreview(ItemInfo item);
        void ResetItemPreview();
    }
}
