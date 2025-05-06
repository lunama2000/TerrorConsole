using UnityEngine;

namespace TerrorConsole
{
    public interface ILocalization
    {
        void SetLanguage(string lang);
        string GetLocalizedValue(string key);


    }
}
