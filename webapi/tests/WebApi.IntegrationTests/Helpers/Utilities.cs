namespace NetClock.WebApi.IntegrationTests.Helpers
{
    public static class Utilities
    {
        // Default apiVersion.
        private const string ApiVersion = "v1";

        public static string ComposeUri(string uri, string apiVersion = ApiVersion)
        {
            return $"api/{apiVersion}/{uri}";
        }
    }
}
