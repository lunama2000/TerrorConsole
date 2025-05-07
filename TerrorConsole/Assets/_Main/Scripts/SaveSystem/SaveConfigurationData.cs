using System;

namespace TerrorConsole
{
    [Serializable]
    public class SaveConfigurationData
    {
        public int SfxVolume;
        public int MusicVolume;
        public string LanguageCode;

        public SaveConfigurationData()
        {
            SfxVolume = 1;
            MusicVolume = 1;
            LanguageCode = "EN";
        }

        public SaveConfigurationData(int sfxVolume, int musicVolume, string languageCode)
        {
            SfxVolume = sfxVolume;
            MusicVolume = musicVolume;
            LanguageCode = languageCode;
        }
    }
}
