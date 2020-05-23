namespace NetClock.Application.Common.Authorization.Constants
{
    /// <summary>
    /// Permisos de los controllers.
    /// </summary>
    public static class Permissions
    {
        public static class Accounts
        {
            public const string View = "Permissions.Accounts.View";
            public const string Create = "Permissions.Accounts.Create";
            public const string Update = "Permissions.Accounts.Update";
            public const string Delete = "Permissions.Accounts.Delete";
        }

        public static class Auth
        {
            public const string View = "Permissions.Auth.View";
            public const string Create = "Permissions.Auth.Create";
            public const string Update = "Permissions.Auth.Update";
            public const string Delete = "Permissions.Auth.Delete";
        }

        public static class AdminAccounts
        {
            public const string View = "Permissions.AdminAccounts.View";
            public const string Create = "Permissions.AdminAccounts.Create";
            public const string Update = "Permissions.AdminAccounts.Update";
            public const string Delete = "Permissions.AdminAccounts.Delete";
        }

        public static class AdminRoles
        {
            public const string View = "Permissions.AdminRoles.View";
            public const string Create = "Permissions.AdminRoles.Create";
            public const string Update = "Permissions.AdminRoles.Update";
            public const string Delete = "Permissions.AdminRoles.Delete";
        }
    }
}
