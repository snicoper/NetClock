namespace NetClock.Application.Admin.AdminAccounts.Commands.EditUser
{
    public class EditUserViewModel
    {
        public EditUserViewModel(string slug)
        {
            Slug = slug;
        }

        public string Slug { get; }
    }
}
