using System;

namespace TerrorConsole
{
    [Serializable]
    public class SaveConfigurationData
    {
        public float SfxVolume;
        public float MusicVolume;
        public string LanguageCode;
        public int ResolutionIndex;

        public SaveConfigurationData()
        {
            SfxVolume = 1;
            MusicVolume = 1;
            LanguageCode = "EN";
        }

        public SaveConfigurationData(float sfxVolume, float musicVolume, string languageCode, int resolutionIndex)
        {
            SfxVolume = sfxVolume;
            MusicVolume = musicVolume;
            LanguageCode = languageCode;
            ResolutionIndex = resolutionIndex;
        }
    }
}
