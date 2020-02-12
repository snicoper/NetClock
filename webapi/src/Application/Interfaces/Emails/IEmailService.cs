using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NetClock.Application.Interfaces.Emails
{
    public interface IEmailService
    {
        MailAddress From { get; set; }

        IList<MailAddress> To { get; set; }

        string Subject { get; set; }

        string Body { get; set; }

        bool IsHtml { get; set; }

        Task SendEmailAsync<TModel>(string viewName, TModel model);

        Task SendEmailAsync();
    }
}
