namespace NetClock.Application.Common.Configurations
{
    public class SmtpConfig
    {
        public string DefaultFrom { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool EnableSsl { get; set; }
    }
}
