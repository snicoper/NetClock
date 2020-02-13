namespace NetClock.Application.Common.Configurations
{
    public class AppSettings
    {
        public Jwt Jwt { get; set; }

        public Smtp Smtp { get; set; }

        public WebApi WebApi { get; set; }

        public WebApp WebApp { get; set; }
    }

    public class Jwt
    {
        public string Secret { get; set; }

        public string ValidIssuer { get; set; }

        public string ValidAudience { get; set; }

        public int ExpiryMinutes { get; set; }
    }

    public class Smtp
    {
        public string DefaultFrom { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool EnableSsl { get; set; }
    }

    public class WebApi
    {
        public string SiteName { get; set; }

        public string Scheme { get; set; }

        public string Host { get; set; }

        public string ApiSegment { get; set; }
    }

    public class WebApp
    {
        public string Scheme { get; set; }

        public string Host { get; set; }
    }
}
