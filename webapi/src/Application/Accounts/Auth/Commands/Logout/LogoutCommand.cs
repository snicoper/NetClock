using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Auth.Commands.Logout
{
    public class LogoutCommand : IRequest
    {
        public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
        {
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly ILogger<LogoutCommandHandler> _logger;

            public LogoutCommandHandler(
                SignInManager<ApplicationUser> signInManager,
                IHttpContextAccessor httpContextAccessor,
                ILogger<LogoutCommandHandler> logger)
            {
                _signInManager = signInManager;
                _httpContextAccessor = httpContextAccessor;
                _logger = logger;
            }

            public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("El usuario va a desconectarse");

                await _signInManager.SignOutAsync();
                await _httpContextAccessor.HttpContext.SignOutAsync();

                _logger.LogInformation("User is logged out.");

                return Unit.Value;
            }
        }
    }
}
