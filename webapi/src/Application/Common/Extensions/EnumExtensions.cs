using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NetClock.Application.Common.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Obtener el valor de [Display(Name = "")] en un Enum.
        /// </summary>
        public static string EnumDisplayNameFor(this Enum item)
        {
            var type = item.GetType();
            var member = type.GetMember(item.ToString());
            var displayAttribute = (DisplayAttribute)member[0]
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault();

            return displayAttribute.Name;
        }
    }
}
