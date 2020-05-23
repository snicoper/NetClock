using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NetClock.Application.Common.Api;
using NetClock.Application.Common.Authorization;

namespace NetClock.Application.Common.Utils
{
    public static class ReflectionUtils
    {
        /// <summary>
        /// Obtener todos los valores de las contantes de una clase concreta.
        /// </summary>
        /// <see cref="PermissionsHelper"/>
        /// <typeparam name="T">Tipo de valor de las contantes.</typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetAllConstantValues<T>(this IReflect type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
                .Select(x => (T)x.GetRawConstantValue())
                .ToList();
        }

        /// <summary>
        /// Obtener todos los nombres de los controllers eliminando la parte Controller.
        /// </summary>
        /// <param name="assembly">Assembly donde buscara los nombres de Controller.</param>
        /// <example>ReflectionUtils.GetAllControllerNames(Assembly.GetEntryAssembly());</example>
        /// <returns>Lista de nombres de todos los controllers.</returns>
        public static IEnumerable<string> GetAllControllerNames(Assembly assembly)
        {
            var assemblyTypes = assembly.GetTypes().Where(type => typeof(ApiControllerBase).IsAssignableFrom(type));

            return assemblyTypes
                .Select(assemblyType => assemblyType.Name)
                .Select(controllerName =>
                    controllerName.Substring(0, controllerName.IndexOf("Controller", StringComparison.Ordinal)))
                .ToList();
        }
    }
}
