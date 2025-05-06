using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

namespace TerrorConsole
{
    public class LanguageCarouselSelector : MonoBehaviour
    { 
        [Header("UI Elements")] 
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private TMP_Text _languageLabel;
        
        private int _currentIndex = 0;

        private void Start()
        {
            if (LocalizationExtensions.LanguageCodes.Length != LocalizationExtensions.LanguageNames.Length)
            {
                Debug.LogError("Language codes and names must be the same length.".Localize());
                return;
            }

            _leftButton.onClick.AddListener(SelectPrevious);
            _rightButton.onClick.AddListener(SelectNext);

            string savedLang = PlayerPrefs.GetString("language", LocalizationExtensions.LanguageCodes[0]);
            _currentIndex = System.Array.IndexOf(LocalizationExtensions.LanguageCodes, savedLang);
            if (_currentIndex < 0) _currentIndex = 0;

            ApplyLanguage();
        }

        private void SelectPrevious()
        {
            _currentIndex--;
            if (_currentIndex < 0)
                _currentIndex = LocalizationExtensions.LanguageCodes.Length - 1;

            ApplyLanguage();
        }

        private void SelectNext()
        {
            _currentIndex++;
            if (_currentIndex >= LocalizationExtensions.LanguageCodes.Length)
                _currentIndex = 0;

            ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            string langCode = LocalizationExtensions.LanguageCodes[_currentIndex];
            string langName = LocalizationExtensions.LanguageNames[_currentIndex];

            _languageLabel.text = langName;
            LocalizationManager.Source.SetLanguage(langCode);
            PlayerPrefs.SetString("language", langCode);
            PlayerPrefs.Save();
        }
    }
}