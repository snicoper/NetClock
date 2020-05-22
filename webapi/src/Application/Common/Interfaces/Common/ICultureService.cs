using System.Collections.Generic;
using System.Globalization;

namespace NetClock.Application.Common.Interfaces.Common
{
    public interface ICultureService
    {
        /// <summary>
        /// Get a list of available cultures in the application.
        /// </summary>
        IEnumerable<CultureInfo> GetCultures();

        /// <summary>
        /// Get current culture.
        /// </summary>
        CultureInfo GetCurrentCulture();
    }
}
