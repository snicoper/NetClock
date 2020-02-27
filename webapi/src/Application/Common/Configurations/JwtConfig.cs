namespace NetClock.Application.Common.Configurations
{
    public class JwtConfig
    {
        public string Secret { get; set; }

        public string ValidIssuer { get; set; }

        public string ValidAudience { get; set; }

        public int ExpiryMinutes { get; set; }
    }
}
