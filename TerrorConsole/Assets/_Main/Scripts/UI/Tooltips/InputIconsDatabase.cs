using System.Collections.Generic;
using UnityEngine;

namespace TerrorConsole
{
    [CreateAssetMenu(fileName = "InputIconsDatabase", menuName = "ScriptableObjects/InputIconsDatabase")]

    public class InputIconsDatabase : ScriptableObject
    {
        public List<KeyCodeIcons> KeyCodesIcons = new List<KeyCodeIcons>();

        public Sprite GetKeyCodeIcon(KeyCode keyCodeToGet)
        {
            foreach (KeyCodeIcons keyCodeIcon in KeyCodesIcons)
            {
                if(keyCodeIcon.KeyCode == keyCodeToGet)
                {
                    return keyCodeIcon.Icon;
                }
            }
            Debug.LogWarning($"Couldn't find any icon for {keyCodeToGet}");
            return null;
        }
    }

    [System.Serializable]
    public struct KeyCodeIcons
    {
        public KeyCode KeyCode;
        public Sprite Icon;
    }
}
