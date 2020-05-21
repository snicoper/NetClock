namespace NetClock.Application.Admin.AdminAccounts.Commands.CreateAccount
{
    public class CreateUserDto
    {
        public CreateUserDto(string slug)
        {
            Slug = slug;
        }

        public string Slug { get; }
    }
}
