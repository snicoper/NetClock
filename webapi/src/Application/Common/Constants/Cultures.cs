using System.Collections.Generic;
using System.Globalization;

namespace NetClock.Application.Common.Constants
{
    public static class Cultures
    {
        public static readonly CultureInfo DefaultCulture = new("es-ES");

        public static readonly IList<CultureInfo> SupportedCultures = new List<CultureInfo>
        {
            new("es-ES"),
            new("ca-ES"),
            new("en-GB")
        };
    }
}
