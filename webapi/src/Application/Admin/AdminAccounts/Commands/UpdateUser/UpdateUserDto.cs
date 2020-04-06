namespace NetClock.Application.Admin.AdminAccounts.Commands.UpdateUser
{
    public class UpdateUserDto
    {
        public UpdateUserDto(string slug)
        {
            Slug = slug;
        }

        public string Slug { get; }
    }
}
