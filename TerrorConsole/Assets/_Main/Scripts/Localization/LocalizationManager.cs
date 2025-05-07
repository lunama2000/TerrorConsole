using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TerrorConsole
{
    [DefaultExecutionOrder(-1000)]
    public class LocalizationManager : Singleton<ILocalization>, ILocalization
    {
        [Header("CSV Settings")] [Tooltip("CSV file must be inside the Resources folder")] [SerializeField]
        private string _csvFileName = "Localization";

        private readonly Dictionary<string, Dictionary<string, string>> _localizationData = new();
        private string _currentLanguage = "EN";

        public string CurrentLanguage => _currentLanguage;
        public event Action OnLanguageChanged;
        
        protected override void Awake()
        {
            base.Awake();
            LoadCsvData();
        }

        private void LoadCsvData()
        {
            _localizationData.Clear();

            TextAsset csvFile = Resources.Load<TextAsset>(_csvFileName);
            if (csvFile == null)
            {
                Debug.LogError($"Localization CSV file '{_csvFileName}' not found in Resources.");
                return;
            }

            using StringReader reader = new(csvFile.text);

            string headerLine = reader.ReadLine();
            if (headerLine == null)
            {
                Debug.LogError("CSV file is empty.");
                return;
            }

            string[] headers = ParseCsvLine(headerLine);
            if (headers.Length < 2)
            {
                Debug.LogError("CSV must contain a 'Key' and at least one language column.");
                return;
            }

            while (reader.Peek() > -1)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] fields = ParseCsvLine(line);
                if (fields.Length != headers.Length)
                {
                    Debug.LogWarning($"Line skipped due to mismatch in column count: {line}");
                    continue;
                }

                string key = fields[0];
                for (int i = 1; i < headers.Length; i++)
                {
                    string lang = headers[i];
                    string value = fields[i];

                    if (!_localizationData.ContainsKey(lang))
                        _localizationData[lang] = new Dictionary<string, string>();

                    _localizationData[lang][key] = value;
                }
            }

            Debug.Log("Localization CSV loaded successfully.");
        }
        
        private static string[] ParseCsvLine(string line)
        {
            List<string> fields = new();
            bool inQuotes = false;
            string currentField = "";
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '\"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '\"')
                    {
                        currentField += '\"';
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    fields.Add(currentField);
                    currentField = "";
                }
                else
                {
                    currentField += c;
                }
            }

            fields.Add(currentField);
            return fields.ToArray();
        }

        public void SetLanguage(string languageCode)
        {
            if (string.IsNullOrWhiteSpace(languageCode) || languageCode == _currentLanguage)
                return;

            if (!_localizationData.ContainsKey(languageCode))
            {
                Debug.LogWarning($"Language '{languageCode}' not found in localization data.");
                return;
            }

            SaveSystemManager.Source.SaveLanguageCode(languageCode);
            _currentLanguage = languageCode;
            OnLanguageChanged?.Invoke();
        }

        public string GetLocalizedText(string key)
        {
            if (_localizationData.TryGetValue(_currentLanguage, out var langDict))
            {
                if (langDict.TryGetValue(key, out var value))
                {
                    return value;
                }
                Debug.LogWarning($"[Localization] Key not found in {_currentLanguage}: {key}");
            }

            return $"#{key}";
        }
        
    }
}