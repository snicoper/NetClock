namespace NetClock.Application.Admin.AdminAccounts.Commands.UpdateUser
{
    public class UpdateUserViewModel
    {
        public UpdateUserViewModel(string slug)
        {
            Slug = slug;
        }

        public string Slug { get; }
    }
}
