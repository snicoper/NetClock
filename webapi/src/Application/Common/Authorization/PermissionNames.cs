namespace NetClock.Application.Common.Authorization
{
    public static class PermissionNames
    {
        public const string Superuser = nameof(Superuser);
        public const string Admin = nameof(Admin);
        public const string Staff = nameof(Staff);
        public const string Employee = nameof(Employee);

        public const string UsersView = nameof(UsersView);
        public const string UsersCreate = nameof(UsersCreate);
        public const string UsersEdit = nameof(UsersEdit);
        public const string UsersDelete = nameof(UsersDelete);

        public const string RolesView = nameof(RolesView);
        public const string RolesCreate = nameof(RolesCreate);
        public const string RolesEdit = nameof(RolesEdit);
        public const string RolesDelete = nameof(RolesDelete);
    }
}
