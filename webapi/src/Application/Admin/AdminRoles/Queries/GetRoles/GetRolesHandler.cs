using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NetClock.Application.Common.Http;
using NetClock.Application.Common.Interfaces.Http;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminRoles.Queries.GetRoles
{
    public class GetRolesHandler : IRequestHandler<GetRolesQuery, ResponseData<GetRolesDto>>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IResponseDataService<ApplicationRole, GetRolesDto> _responseDataService;

        public GetRolesHandler(
            RoleManager<ApplicationRole> roleManager,
            IResponseDataService<ApplicationRole, GetRolesDto> responseDataService)
        {
            _roleManager = roleManager;
            _responseDataService = responseDataService;
        }

        public async Task<ResponseData<GetRolesDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = _roleManager.Roles;

            return await _responseDataService.CreateAsync(roles, request.RequestData, cancellationToken);
        }
    }
}
