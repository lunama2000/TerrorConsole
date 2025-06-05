using UnityEngine;

namespace TerrorConsole
{
    [CreateAssetMenu(fileName = "ItemInfo", menuName = "ScriptableObjects/ItemInfo")]
    public class ItemInfo : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private Sprite itemSprite;
        [SerializeField] private string itemDescription;
        [SerializeField] private bool hasPreviewInInventory = false;
        [SerializeField] private bool isBossItem = false;

        public string ItemName => itemName;
        public Sprite ItemSprite => itemSprite;
        public string ItemDescription => itemDescription;

        public bool HasPreviewInInventory => hasPreviewInInventory;
        public bool IsBossItem => isBossItem;
        

        public virtual void OnClickedInInventory()
        {
        }
    }
}
