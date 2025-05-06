using UnityEngine;
using TMPro;

public class LocalizeText : MonoBehaviour
{
    public string localizationKey;

    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (!string.IsNullOrEmpty(localizationKey))
        {
            GetComponent<TextMeshProUGUI>().text = LocalizationManager.Source.GetLocalizedValue(localizationKey);
        }
    }
}