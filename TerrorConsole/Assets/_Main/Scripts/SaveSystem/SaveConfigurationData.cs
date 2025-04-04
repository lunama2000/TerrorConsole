using System;
using UnityEngine;

namespace TerrorConsole
{
    [Serializable]
    public class SaveConfigurationData
    {
        public int SfxVolume;
        public int MusicVolume;
        public int LanguageIndex;

        public SaveConfigurationData()
        {
            SfxVolume = 1;
            MusicVolume = 1;
            LanguageIndex = 0;
        }

        public SaveConfigurationData(int sfxVolume, int musicVolume, int lenguageIndex)
        {
            SfxVolume = sfxVolume;
            MusicVolume = musicVolume;
            LanguageIndex = lenguageIndex;
        }
    }
}
