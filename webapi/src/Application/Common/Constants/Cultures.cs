using System.Collections.Generic;
using System.Globalization;

namespace NetClock.Application.Common.Constants
{
    public static class Cultures
    {
        public static readonly CultureInfo DefaultCulture = new CultureInfo("es-ES");

        public static readonly IList<CultureInfo> SupportedCultures = new List<CultureInfo>
        {
            new CultureInfo("es-ES"),
            new CultureInfo("ca-ES"),
            new CultureInfo("en-GB")
        };
    }
}
