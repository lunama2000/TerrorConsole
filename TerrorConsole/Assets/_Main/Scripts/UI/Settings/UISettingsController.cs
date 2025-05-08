using System.Collections.Generic;
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
            GenerateLanguajes();
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
            SaveSystemManager.Source.SaveSFXVolume(newVolume);
        }

        private void SetMusicVolume(float newVolume)
        {
            AudioManager.Source.SetMusicVolume(newVolume);
            SaveSystemManager.Source.SaveMusicVolume(newVolume);
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
            int resolucionActual = 0;

            for (int i = 0; i < _resolutions.Length; i++)
            {
                string option = _resolutions[i].width + " x " + _resolutions[i].height;
                options.Add(option);


                if (Screen.fullScreen && _resolutions[i].width == Screen.currentResolution.width &&
                    _resolutions[i].height == Screen.currentResolution.height)
                {
                    resolucionActual = i;
                }

            }

            _resolutionsDropdown.AddOptions(options);
            _resolutionsDropdown.value = resolucionActual;
            _resolutionsDropdown.RefreshShownValue();

            _resolutionsDropdown.value = SaveSystemManager.Source.GetSavedResolution();
        }

        private void GenerateLanguajes()
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
            int index = _resolutionsDropdown.value;
            SaveSystemManager.Source.SaveResolution(index);
            
            Resolution resolution = _resolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }
}
