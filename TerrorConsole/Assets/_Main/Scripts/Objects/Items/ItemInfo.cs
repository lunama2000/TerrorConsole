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

        public string ItemName => itemName;
        public Sprite ItemSprite => itemSprite;
        public string ItemDescription => itemDescription;

        public bool HasPreviewInInventory => hasPreviewInInventory;

        public virtual void OnClickedInInventory()
        {
        }
    }
}
