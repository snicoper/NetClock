namespace NetClock.Application.Admin.AdminAccounts.Commands.CreateUser
{
    public class CreateUserViewModel
    {
        public CreateUserViewModel(string slug)
        {
            Slug = slug;
        }

        public string Slug { get; }
    }
}
