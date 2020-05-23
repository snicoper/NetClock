using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NetClock.Application.Common.Http;
using NetClock.Application.Common.Interfaces.Http;

namespace NetClock.Application.Admin.AdminRoles.Queries.GetRoles
{
    public class GetRolesHandler : IRequestHandler<GetRolesQuery, ResponseData<GetRolesDto>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IResponseDataService<IdentityRole, GetRolesDto> _responseDataService;

        public GetRolesHandler(
            RoleManager<IdentityRole> roleManager,
            IResponseDataService<IdentityRole, GetRolesDto> responseDataService)
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
