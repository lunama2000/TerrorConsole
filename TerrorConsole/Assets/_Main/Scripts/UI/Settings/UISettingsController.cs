using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TerrorConsole
{
    public class UISettingsController : MonoBehaviour
    {
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Slider _musicSlider;

        [SerializeField] private Toggle _fullScreenToggle;

        [SerializeField] private TMP_Dropdown _resolutionsDropdown;
        private Resolution[] _resolutions;

        [SerializeField] private TMP_Dropdown _languageDropdown;
        [SerializeField] private List<string> _languageKeys = new List<string>();

        private void Start()
        {
            _sfxSlider.value = SaveSystemManager.Source.GetSavedSFXVolume();
            _musicSlider.value = SaveSystemManager.Source.GetSavedMusicVolume();

            if (Screen.fullScreen)
            {
                _fullScreenToggle.isOn = true;
            }
            else
            {
                _fullScreenToggle.isOn = false;
            }
            GenerateResolutions();
            GenerateLanguages();
        }

        public void UpdateSFXVolumeFromSlider()
        {
            SetSFXVolume(_sfxSlider.value);
        }

        public void UpdateMusicVolumeFromSlider()
        {
            SetMusicVolume(_musicSlider.value);
        }

        private void SetSFXVolume(float newVolume)
        {
            AudioManager.Source.SetSFXVolume(newVolume);
        }

        private void SetMusicVolume(float newVolume)
        {
            AudioManager.Source.SetMusicVolume(newVolume);
        }

        public void ToogleFullScreen()
        {
            Screen.fullScreen = _fullScreenToggle.isOn;
        }

        private void GenerateResolutions()
        {
            _resolutions = Screen.resolutions;
            _resolutionsDropdown.ClearOptions();
            List<string> options = new List<string>();
            int currentResolution = 0;

            for (int i = 0; i < _resolutions.Length; i++)
            {
                string option = $"{_resolutions[i].width } x {_resolutions[i].height} {_resolutions[i].refreshRateRatio}hz";
                
                options.Add(option);
                
                if (Screen.fullScreen && _resolutions[i].width == Screen.currentResolution.width &&
                    _resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolution = i;
                }

            }

            _resolutionsDropdown.AddOptions(options);
            _resolutionsDropdown.value = currentResolution;
            _resolutionsDropdown.RefreshShownValue();

            _resolutionsDropdown.value = SaveSystemManager.Source.GetSavedResolution();
        }

        private void GenerateLanguages()
        {
            _languageDropdown.ClearOptions();
            _languageDropdown.AddOptions(_languageKeys);
            _languageDropdown.RefreshShownValue();

            string savedLanguageKey = SaveSystemManager.Source.GetSavedLanguageCode();
            int languageIndex = 0;
            for (int i = 0; i < _languageKeys.Count; i++)
            {
                if (_languageKeys[i] == savedLanguageKey)
                {
                    languageIndex = i;
                }
            }
            _languageDropdown.value = languageIndex;
        }

        public void ChangeLanguage()
        {
            SaveSystemManager.Source.SaveLanguageCode(_languageKeys[_languageDropdown.value]);
            LocalizationManager.Source.SetLanguage(_languageKeys[_languageDropdown.value]);
        }

        public void ChangeResolution()
        {
            ChangeResolutionAsync().Forget();
        }

        public async UniTask ChangeResolutionAsync()
        {

            _resolutionsDropdown.interactable = false;


            int index = _resolutionsDropdown.value;
            SaveSystemManager.Source.SaveResolution(index);

            Resolution resolution = _resolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);


            await UniTask.Delay(500);


            _resolutionsDropdown.interactable = true;

        }
    }
}
