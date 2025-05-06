namespace TerrorConsole
{
    public static class LocalizationExtensions 
    {
        public static readonly string[] LanguageCodes = { "EN", "ES", "PT"};
        public static readonly string[] LanguageNames = { "English", "Español", "Português" };
        
        public static string Localize(this string key)
        {
            return LocalizationManager.Source.GetLocalizedText(key); 
        }
    }
}
