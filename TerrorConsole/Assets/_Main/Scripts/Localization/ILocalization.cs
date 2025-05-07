using System;
using UnityEngine;

namespace TerrorConsole
{
    public interface ILocalization
    {
        void SetLanguage(string languageCode);
        string GetLocalizedText(string key);
        string CurrentLanguage { get; }
        event Action OnLanguageChanged;

    }
}
