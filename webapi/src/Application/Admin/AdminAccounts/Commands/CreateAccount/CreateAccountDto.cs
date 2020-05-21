namespace NetClock.Application.Admin.AdminAccounts.Commands.CreateAccount
{
    public class CreateAccountDto
    {
        public CreateAccountDto(string slug)
        {
            Slug = slug;
        }

        public string Slug { get; }
    }
}
