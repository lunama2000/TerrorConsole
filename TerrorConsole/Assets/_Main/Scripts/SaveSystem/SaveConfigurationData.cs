using System;
using UnityEngine;

namespace TerrorConsole
{
    [Serializable]
    public class SaveConfigurationData
    {
        public int _sfxVolume;
        public int _musicVolume;
        public int _lenguageIndex;

        public SaveConfigurationData()
        {
            _sfxVolume = 1;
            _musicVolume = 1;
            _lenguageIndex = 0;
        }

        public SaveConfigurationData(int sfxVolume, int musicVolume, int lenguageIndex)
        {
            _sfxVolume = sfxVolume;
            _musicVolume = musicVolume;
            _lenguageIndex = lenguageIndex;
        }
    }
}
