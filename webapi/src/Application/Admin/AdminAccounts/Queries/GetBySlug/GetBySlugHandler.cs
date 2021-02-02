using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Exceptions;
using NetClock.Application.Common.Interfaces.Identity;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Queries.GetBySlug
{
    public class GetBySlugHandler : IRequestHandler<GetBySlugQuery, GetBySlugDto>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<GetBySlugHandler> _logger;

        public GetBySlugHandler(IIdentityService identityService, ILogger<GetBySlugHandler> logger)
        {
            _identityService = identityService;
            _logger = logger;
        }

        public async Task<GetBySlugDto> Handle(GetBySlugQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Se va a obtener el usuario {slug}.", request.Slug);
            var user = await _identityService.FirstOrDefaultBySlugAsync(request.Slug);

            if (user is not null)
            {
                return user.Adapt<GetBySlugDto>();
            }

            _logger.LogWarning("El usuario {slug} no existe en la base de datos.", request.Slug);
            throw new NotFoundException(nameof(ApplicationUser), request.Slug);
        }
    }
}
