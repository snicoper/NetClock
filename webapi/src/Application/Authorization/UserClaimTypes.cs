using System.Security.Claims;

namespace NetClock.Application.Authorization
{
    public static class UserClaimTypes
    {
        public const string UserName = ClaimTypes.Name;

        public const string UserId = ClaimTypes.NameIdentifier;

        public const string SerialNumber = ClaimTypes.SerialNumber;

        public const string Role = ClaimTypes.Role;

        public const string Email = ClaimTypes.Email;

        public const string DisplayName = nameof(DisplayName);

        public const string Permission = nameof(Permission);

        public const string PackedPermission = nameof(PackedPermission);

        public const string BranchId = nameof(BranchId);

        public const string BranchName = nameof(BranchName);

        public const string IsHeadOffice = nameof(IsHeadOffice);

        public const string TenantId = nameof(TenantId);

        public const string TenantName = nameof(TenantName);

        public const string IsHeadTenant = nameof(IsHeadTenant);

        public const string ImpersonatorUserId = nameof(ImpersonatorUserId);

        public const string ImpersonatorTenantId = nameof(ImpersonatorTenantId);
    }
}
