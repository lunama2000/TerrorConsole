namespace TerrorConsole
{
    public static class LocalizationExtensions 
    {
        public static readonly string[] LanguageCodes = { "EN", "ES" };
        public static readonly string[] LanguageNames = { "English", "Español" };
        
        public static string Localize(this string key)
        {
            return LocalizationManager.Source.GetLocalizedText(key); 
        }
    }
}
