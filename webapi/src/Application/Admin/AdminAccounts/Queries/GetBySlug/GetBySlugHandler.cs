using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Exceptions;
using NetClock.Application.Common.Interfaces.Identity;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Queries.GetBySlug
{
    public class GetBySlugHandler : IRequestHandler<GetBySlugQuery, AdminUserDetailsViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly ILogger<GetBySlugHandler> _logger;

        public GetBySlugHandler(IMapper mapper, IIdentityService identityService, ILogger<GetBySlugHandler> logger)
        {
            _mapper = mapper;
            _identityService = identityService;
            _logger = logger;
        }

        public async Task<AdminUserDetailsViewModel> Handle(GetBySlugQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Se va a obtener el usuario {request.Slug}");
            var user = await _identityService.FirstOrDefaultBySlugAsync(request.Slug);

            if (user != null)
            {
                return _mapper.Map<AdminUserDetailsViewModel>(user);
            }

            _logger.LogWarning($"El usuario {request.Slug} no existe en la base de datos");
            throw new NotFoundException(nameof(ApplicationUser), request.Slug);
        }
    }
}
