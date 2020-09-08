using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NetClock.Application.Common.Api;

namespace NetClock.Application.Common.Utils
{
    public static class ReflectionUtils
    {
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
                .Select(
                    controllerName => controllerName.Substring(0, controllerName.IndexOf("Controller", StringComparison.Ordinal)))
                .ToList();
        }
    }
}
