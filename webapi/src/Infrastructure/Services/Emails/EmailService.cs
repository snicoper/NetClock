using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Interfaces.Emails;
using NetClock.Application.Common.Interfaces.Views;
using NetClock.Application.Common.Options;

namespace NetClock.Infrastructure.Services.Emails
{
    public class EmailService : IEmailService
    {
        private readonly SmtpOptions _smtpOptions;
        private readonly ILogger<EmailService> _logger;
        private readonly IViewRenderService _viewRenderService;
        private readonly IWebHostEnvironment _environment;

        public EmailService(
            IOptions<SmtpOptions> appSettings,
            ILogger<EmailService> logger,
            IViewRenderService viewRenderService,
            IWebHostEnvironment environment)
        {
            _smtpOptions = appSettings.Value;
            _logger = logger;
            _viewRenderService = viewRenderService;
            _environment = environment;
            To = new List<MailAddress>();
        }

        public MailAddress From { get; set; }

        public IList<MailAddress> To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsHtml { get; set; }

        /// <summary>
        /// Envía un email con un template.
        /// </summary>
        /// <param name="viewName">Vista cshtml.</param>
        /// <param name="model">Modelo de datos para la vista.</param>
        public async Task SendEmailAsync<TModel>(string viewName, TModel model)
        {
            Body = await _viewRenderService.RenderToStringAsync(viewName, model);
            await SendEmailAsync();
        }

        /// <summary>
        /// Envía un email.
        /// </summary>
        public async Task SendEmailAsync()
        {
            Validate();
            From ??= new MailAddress(_smtpOptions.DefaultFrom);

            if (_environment.IsDevelopment() || _environment.IsEnvironment(CommonConstants.Test))
            {
                LoggerEmailInConsole();

                return;
            }

            using var smtpClient = new SmtpClient();
            var credentials = new NetworkCredential
            {
                UserName = _smtpOptions.UserName,
                Password = _smtpOptions.Password
            };

            smtpClient.Credentials = credentials;
            smtpClient.Host = _smtpOptions.Host;
            smtpClient.Port = _smtpOptions.Port;
            smtpClient.EnableSsl = _smtpOptions.EnableSsl;

            using var emailMessage = new MailMessage();
            foreach (var emailTo in To)
            {
                emailMessage.To.Add(emailTo);
            }

            emailMessage.IsBodyHtml = IsHtml;
            emailMessage.From = From;
            emailMessage.Subject = Subject;
            emailMessage.Body = Body;

            await smtpClient.SendMailAsync(emailMessage);
            _logger.LogInformation("Enviado email a {to}.", string.Join(", ", To));
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Subject))
            {
                throw new ArgumentNullException($"El campo {nameof(Subject)} es requerido");
            }

            if (string.IsNullOrEmpty(Body))
            {
                throw new ArgumentNullException($"El campo {nameof(Body)} es requerido");
            }

            if (!To.Any())
            {
                throw new ArgumentNullException($"El campo {nameof(To)} al menos requiere un destinatario");
            }
        }

        /// <summary>
        /// Muestra el log del email en consola.
        /// </summary>
        private void LoggerEmailInConsole()
        {
            var toList = To.Select(to => to.Address).ToList();
            var logEmail = new StringBuilder();
            logEmail.Append("\n=============================================\n");
            logEmail.Append($"Subject: {Subject}\n");
            logEmail.Append($"From: {From}\n");
            logEmail.Append("TO: ");
            logEmail.Append(string.Join(", ", toList));
            logEmail.Append("\n=============================================\n");
            logEmail.Append($"{Body}\n");
            logEmail.Append("=============================================\n");
            _logger.LogInformation(logEmail.ToString());
        }
    }
}
