using System;
using System.Collections.Generic;
using System.Linq;
using NetClock.Application.Common.Extensions;

namespace NetClock.Application.Common.Authorization.Constants
{
    public static class PermissionsHelper
    {
        public static IEnumerable<string> GetAllPermissionValues()
        {
            var types = GetPermissionTypes();

            return types.SelectMany(ReflectionExtensions.GetAllConstantValues<string>).ToList();
        }

        private static IEnumerable<Type> GetPermissionTypes()
        {
            return typeof(Permissions).GetNestedTypes();
        }
    }
}
