using UnityEngine;

namespace TerrorConsole
{
    [CreateAssetMenu(fileName = "ItemInfo", menuName = "ScriptableObjects/ItemInfo")]
    public class ItemInfo : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private Sprite itemSprite;
        [SerializeField] private string itemDescription;

        public string ItemName => itemName;
        public Sprite ItemSprite => itemSprite;
        public string ItemDescription => itemDescription;

        public virtual void OnClickedInInventory()
        {
        }
    }
}
