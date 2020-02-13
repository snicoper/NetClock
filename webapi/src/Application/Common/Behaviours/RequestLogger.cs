using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Interfaces.Identity;

namespace NetClock.Application.Common.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;

        public RequestLogger(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? "0";
            var userName = _currentUserService.Name ?? "Anonymous";

            _logger.LogInformation(
                "NetClock Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName,
                userId,
                userName,
                request);

            return Task.CompletedTask;
        }
    }
}
