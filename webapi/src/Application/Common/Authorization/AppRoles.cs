namespace NetClock.Application.Common.Authorization
{
    public abstract class AppRoles
    {
        public const string Superuser = nameof(Superuser);
        public const string Admin = nameof(Admin);
        public const string Staff = nameof(Staff);
        public const string Employee = nameof(Employee);
    }
}
