using System;
using System.Collections.Generic;
using System.Linq;
using NetClock.Application.Common.Authorization.Constants;
using NetClock.Application.Common.Utils;

namespace NetClock.Application.Common.Authorization
{
    public static class PermissionsHelper
    {
        public static IEnumerable<string> GetAllPermissionValues()
        {
            var types = GetPermissionTypes();

            return types.SelectMany(ReflectionUtils.GetAllConstantValues<string>).ToList();
        }

        private static IEnumerable<Type> GetPermissionTypes()
        {
            return typeof(Permissions).GetNestedTypes();
        }
    }
}
