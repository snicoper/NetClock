using System.Collections.Generic;
using System.Text;
using System.Web;
using Microsoft.Extensions.Options;
using NetClock.Application.Common.Interfaces.Common;
using NetClock.Application.Common.Options;

namespace NetClock.Infrastructure.Services.Common
{
    public class LinkGeneratorService : ILinkGeneratorService
    {
        private readonly WebAppOptions _webAppOptions;

        public LinkGeneratorService(IOptions<WebAppOptions> options)
        {
            _webAppOptions = options.Value;
        }

        public string GenerateFrontEnd(string path)
        {
            return $"{_webAppOptions.Scheme}://{_webAppOptions.Host}/{path}";
        }

        public string GenerateFrontEnd(string path, Dictionary<string, string> queryParams, bool encodeParams = true)
        {
            var queryString = new StringBuilder();

            foreach (var (key, value) in queryParams)
            {
                var valueEncode = encodeParams ? HttpUtility.HtmlEncode(value) : value;
                var paramOperator = queryString.Length == 0 ? "?" : "&";
                queryString.Append($"{paramOperator}{key}={valueEncode}");
            }

            return $"{GenerateFrontEnd(path)}{queryString}";
        }
    }
}
