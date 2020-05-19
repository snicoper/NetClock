namespace NetClock.Application.Common.Authorization
{
    public static class Permissions
    {
        public static class Superusers
        {
            public const string Full = "Permissions.Superusers.Full";
            public const string View = "Permissions.Superusers.View";
            public const string Create = "Permissions.Superusers.Create";
            public const string Update = "Permissions.Superusers.Update";
            public const string Delete = "Permissions.Superusers.Delete";
        }

        public static class Admins
        {
            public const string Full = "Permissions.Admins.Full";
            public const string View = "Permissions.Admins.View";
            public const string Create = "Permissions.Admins.Create";
            public const string Update = "Permissions.Admins.Update";
            public const string Delete = "Permissions.Admins.Delete";
        }

        public static class Staffs
        {
            public const string Full = "Permissions.Staffs.Full";
            public const string View = "Permissions.Staffs.View";
            public const string Create = "Permissions.Staffs.Create";
            public const string Update = "Permissions.Staffs.Update";
            public const string Delete = "Permissions.Staffs.Delete";
        }

        public static class Employees
        {
            public const string Full = "Permissions.Employees.Full";
            public const string View = "Permissions.Employees.View";
            public const string Create = "Permissions.Employees.Create";
            public const string Update = "Permissions.Employees.Update";
            public const string Delete = "Permissions.Employees.Delete";
        }
    }
}
