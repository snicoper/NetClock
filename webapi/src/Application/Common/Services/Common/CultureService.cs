using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using NetClock.Application.Common.Interfaces.Common;

namespace NetClock.Application.Common.Services.Common
{
    public class CultureService : ICultureService
    {
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;

        public CultureService(IOptions<RequestLocalizationOptions> localizationOptions)
        {
            _localizationOptions = localizationOptions;
        }

        public IEnumerable<CultureInfo> GetCultures()
        {
            return _localizationOptions.Value.SupportedCultures;
        }

        public CultureInfo GetCurrentCulture()
        {
            return CultureInfo.CurrentCulture;
        }
    }
}
