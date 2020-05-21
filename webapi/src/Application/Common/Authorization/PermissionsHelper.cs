using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NetClock.Application.Common.Authorization
{
    public static class PermissionsHelper
    {
        public static IEnumerable<string> GetAllPermissionValues()
        {
            var types = GetPermissionTypes();

            return types.SelectMany(GetAllPermissionConstantValues<string>).ToList();
        }

        private static IEnumerable<Type> GetPermissionTypes()
        {
            return typeof(Permissions).GetNestedTypes();
        }

        private static IEnumerable<T> GetAllPermissionConstantValues<T>(this IReflect type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
                .Select(x => (T)x.GetRawConstantValue())
                .ToList();
        }
    }
}
