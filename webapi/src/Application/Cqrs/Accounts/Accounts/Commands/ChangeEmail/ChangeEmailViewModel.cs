namespace NetClock.Application.Cqrs.Accounts.Accounts.Commands.ChangeEmail
{
    public class ChangeEmailViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string OldEmail { get; set; }

        public string NewEmail { get; set; }

        public string CallBack { get; set; }
    }
}
