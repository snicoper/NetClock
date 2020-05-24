using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NetClock.Application.Common.Authorization.Constants;

namespace NetClock.Application.Common.Extensions
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Obtener todos los valores de un tipo en las contantes de una clase concreta.
        /// </summary>
        /// <see cref="PermissionsHelper"/>
        /// <typeparam name="T">Tipo de valor de las contantes.</typeparam>
        public static IEnumerable<T> GetAllConstantValues<T>(this IReflect type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
                .Select(x => (T)x.GetRawConstantValue())
                .ToList();
        }
    }
}
