namespace NetClock.Application.Admin.AdminAccounts.Commands.CreateUser
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
