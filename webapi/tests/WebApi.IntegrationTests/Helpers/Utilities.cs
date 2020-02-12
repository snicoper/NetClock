using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        public static async Task<T> GetResponseContentAsync<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        public static StringContent GetRequestContent<T>(T data)
            where T : class
        {
            return new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        }
    }
}
