namespace Kentico.Kontent.Statiq.Lumen.Models
{
    public static class MetadataHelpers
    {
        public static string Cascade(this string preferred, string fallback)
        {
            return string.IsNullOrWhiteSpace(preferred) ? fallback : preferred;
        }
    }
}