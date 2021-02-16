using System.Collections.Generic;
using System.Globalization;

namespace NetClock.Application.Common.Constants
{
    public static class Cultures
    {
        public static readonly CultureInfo Default = new ("es-ES");

        public static readonly IList<CultureInfo> Supported = new List<CultureInfo>
        {
            new ("es-ES"),
            new ("ca-ES"),
            new ("en-GB"),
        };
    }
}
