namespace NetClock.Application.Common.Constants
{
    public static class SupportedCultures
    {
        public const string EsEs = "es-ES";
        public const string EsCa = "ca-ES";
        public const string EnGb = "en-GB";

        public static string GetCulture(string culture)
        {
            return culture switch
            {
                EsEs => EsEs,
                EsCa => EsCa,
                EnGb => EnGb,
                _ => null
            };
        }
    }
}
