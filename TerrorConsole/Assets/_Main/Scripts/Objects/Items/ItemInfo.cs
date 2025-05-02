using UnityEngine;

namespace TerrorConsole
{
    [CreateAssetMenu(fileName = "ItemInfo", menuName = "ScriptableObjects/ItemInfo")]
    public class ItemInfo : ScriptableObject
    {
        public string itemName;
        public Sprite itemSprite;
        public string itemDescription;
    }
}
