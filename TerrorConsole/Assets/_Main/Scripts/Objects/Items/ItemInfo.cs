using UnityEngine;

namespace TerrorConsole
{
    [CreateAssetMenu(fileName = "ItemInfo", menuName = "ScriptableObjects/ItemInfo")]
    public class ItemInfo : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private Sprite itemSprite;
        [SerializeField] private string itemDescription;

        public string ItemName { get => itemName; set => itemName = value; }
        public Sprite ItemSprite { get => itemSprite; set => itemSprite = value; }
        public string ItemDescription { get => itemDescription; set => itemDescription = value; }

        public virtual void OnClickedInInventory()
        {
        }
    }
}
