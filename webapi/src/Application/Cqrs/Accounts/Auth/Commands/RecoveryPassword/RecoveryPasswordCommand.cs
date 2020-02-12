using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetClock.Application.Configurations;
using NetClock.Application.Constants;
using NetClock.Application.Exceptions;
using NetClock.Application.Interfaces.Common;
using NetClock.Application.Interfaces.Emails;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Cqrs.Accounts.Auth.Commands.RecoveryPassword
{
    public class RecoveryPasswordCommand : IRequest
    {
        public RecoveryPasswordCommand(string email)
        {
            Email = email;
        }

        public string Email { get; }

        public class RecoveryPasswordCommandHandler : IRequestHandler<RecoveryPasswordCommand>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ILinkGeneratorService _linkGeneratorService;
            private readonly ILogger<RecoveryPasswordCommandHandler> _logger;
            private readonly IEmailService _emailService;
            private readonly AppSettings _appSettings;

            public RecoveryPasswordCommandHandler(
                UserManager<ApplicationUser> userManager,
                ILinkGeneratorService linkGeneratorService,
                ILogger<RecoveryPasswordCommandHandler> logger,
                IOptions<AppSettings> options,
                IEmailService emailService)
            {
                _userManager = userManager;
                _linkGeneratorService = linkGeneratorService;
                _logger = logger;
                _emailService = emailService;
                _appSettings = options.Value;
            }

            public async Task<Unit> Handle(RecoveryPasswordCommand request, CancellationToken cancellationToken)
            {
                _logger.LogInformation($"El usuario {request.Email} va a recuperar el email.");

                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user is null)
                {
                    _logger.LogWarning($"El email {request.Email} no existe en la base de datos");
                    throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Email));
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var recoveryPasswordViewModel = new RecoveryPasswordViewModel
                {
                    UserName = user.UserName, CallBack = GenerateCallBack(user.Id, code)
                };

                await SendEmailNotificationAsync(user, recoveryPasswordViewModel);
                _logger.LogInformation($"Se a enviado un email a {request.Email} para recuperar la contraseña");

                return Unit.Value;
            }

            private async Task SendEmailNotificationAsync(
                ApplicationUser applicationUser,
                RecoveryPasswordViewModel recoveryPasswordViewModel)
            {
                _emailService.Subject = $"Confirmación de cambio de email en {_appSettings.WebApi.SiteName}";
                _emailService.To.Add(new MailAddress(applicationUser.Email));
                _emailService.IsHtml = true;
                await _emailService.SendEmailAsync(EmailTemplates.RecoveryPassword, recoveryPasswordViewModel);
            }

            private string GenerateCallBack(string id, string code)
            {
                var queryParams = new Dictionary<string, string> { ["userId"] = id, ["code"] = code };

                return _linkGeneratorService.GenerateFrontEnd(
                    UrlsFrontEnd.AccountsRecoveryPasswordValidate,
                    queryParams);
            }
        }
    }
}
