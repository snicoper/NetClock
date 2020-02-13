using System;
using Microsoft.AspNetCore.Authorization;

namespace NetClock.Application.Common.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        public PermissionAuthorizeAttribute(params string[] permissions)
        {
            Policy = $"{PermissionConstant.PolicyPrefix}{string.Join(PermissionConstant.PolicyNameSplitSymbol, permissions)}";
        }
    }
}
