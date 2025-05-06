using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageCarouselSelector : MonoBehaviour
{
    public List<string> availableLanguages = new List<string> { "EN", "ES", "PT" };

    public TextMeshProUGUI languageLabel;
    public Button leftButton;
    public Button rightButton;

    private int currentIndex = 0;

    void Start()
    {
        string currentLang = LocalizationManager.Instance.currentLanguage;
        currentIndex = availableLanguages.IndexOf(currentLang);
        if (currentIndex < 0) currentIndex = 0;

        UpdateLanguageLabel();

        leftButton.onClick.AddListener(OnLeftPressed);
        rightButton.onClick.AddListener(OnRightPressed);
    }

    void OnLeftPressed()
    {
        currentIndex = (currentIndex - 1 + availableLanguages.Count) % availableLanguages.Count;
        ApplyLanguageChange();
    }

    void OnRightPressed()
    {
        currentIndex = (currentIndex + 1) % availableLanguages.Count;
        ApplyLanguageChange();
    }

    void ApplyLanguageChange()
    {
        string selectedLang = availableLanguages[currentIndex];
        LocalizationManager.Instance.SetLanguage(selectedLang);
        UpdateLanguageLabel();
        RefreshLocalizedTexts();
    }

    void UpdateLanguageLabel()
    {
        languageLabel.text = availableLanguages[currentIndex];
    }

    void RefreshLocalizedTexts()
    {
        foreach (LocalizeText lt in FindObjectsOfType<LocalizeText>())
        {
            lt.UpdateText();
        }
    }
}