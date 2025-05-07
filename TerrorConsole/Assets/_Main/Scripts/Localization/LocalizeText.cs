using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class LocalizeText : MonoBehaviour
    {
       [Tooltip("Key usada para buscar el texto traducido en el archivo CSV.")] 
       [SerializeField] private string _localizationKey;

        private void OnEnable()
        {
            UpdateLocalizedText();
            LocalizationManager.Source.OnLanguageChanged += UpdateLocalizedText;
        }

        private void OnDisable()
        {
            if (LocalizationManager.Source != null)
                LocalizationManager.Source.OnLanguageChanged -= UpdateLocalizedText;
        }

        private void Reset()
        {
            if (TryGetComponent(out Text uiText) && !string.IsNullOrWhiteSpace(uiText.text))
                _localizationKey = uiText.text;
            else if (TryGetComponent(out TMP_Text tmpText) && !string.IsNullOrWhiteSpace(tmpText.text))
                _localizationKey = tmpText.text;
        }

        private void UpdateLocalizedText()
        {
            if (string.IsNullOrWhiteSpace(_localizationKey))
                return;

            string localized = LocalizationManager.Source.GetLocalizedText(_localizationKey);

            if (localized.StartsWith("#"))
            {
                Debug.LogWarning($"Localization key not found: {_localizationKey}");
            }

            if (TryGetComponent(out TMP_Text tmpText))
            {
                tmpText.text = localized;
            }
            else if (TryGetComponent(out Text uiText))
            {
                uiText.text = localized;
            }
            else
            {
                Debug.LogWarning($"No UI Text component found on '{gameObject.name}'.");
            }
        }
    }
}