using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using TerrorConsole;
using UnityEngine.Serialization;

public class LocalizationManager : Singleton<ILocalization>, ILocalization
{
    public static LocalizationManager Instance;

    public string defaultLanguage = "EN";
    public string currentLanguage = "EN";
    
    private Dictionary<string, Dictionary<string, string>> _localizedTexts = new Dictionary<string, Dictionary<string, string>>();

    protected override void Awake()
    {
            LoadLocalizationFile("Localization");
    }

    public void SetLanguage(string lang)
    {
        currentLanguage = lang;
    }

    public string GetLocalizedValue(string key)
    {
        if (_localizedTexts.ContainsKey(key))
        {
            if (_localizedTexts[key].ContainsKey(currentLanguage))
            {
                return _localizedTexts[key][currentLanguage];
            }
            else if (_localizedTexts[key].ContainsKey(defaultLanguage))
            {
                return _localizedTexts[key][defaultLanguage];
            }
        }

        return $"#{key}#";
    }

    private void LoadLocalizationFile(string fileName)
    {
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);
        if (csvFile == null)
        {
            Debug.LogError("Localization CSV file not found!");
            return;
        }

        string[] lines = csvFile.text.Split('\n');
        if (lines.Length <= 1) return;

        string[] headers = lines[0].Trim().Split(',');

        for (int i = 1; i < lines.Length; i++)
        {
            string[] entries = ParseCsvLine(lines[i]);

            if (entries.Length != headers.Length)
                continue;

            string key = entries[0].Trim();
            var langDict = new Dictionary<string, string>();

            for (int j = 1; j < headers.Length; j++)
            {
                langDict[headers[j].Trim()] = entries[j].Trim().Replace("\\n", "\n");
            }

            _localizedTexts[key] = langDict;
        }
    }

    private string[] ParseCsvLine(string line)
    {
        var result = new List<string>();
        bool inQuotes = false;
        StringBuilder field = new StringBuilder();

        foreach (char c in line)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes;
                continue;
            }

            if (c == ',' && !inQuotes)
            {
                result.Add(field.ToString());
                field.Clear();
            }
            else
            {
                field.Append(c);
            }
        }

        result.Add(field.ToString());
        return result.ToArray();
    }
}
