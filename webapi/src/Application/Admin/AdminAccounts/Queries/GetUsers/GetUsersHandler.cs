using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NetClock.Application.Common.Http;
using NetClock.Application.Common.Interfaces.Http;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Queries.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, ResponseData<AdminUserListDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IResponseDataService<ApplicationUser, AdminUserListDto> _responseDataService;

        public GetUsersHandler(
            UserManager<ApplicationUser> userManager,
            IResponseDataService<ApplicationUser, AdminUserListDto> responseDataService)
        {
            _userManager = userManager;
            _responseDataService = responseDataService;
        }

        public async Task<ResponseData<AdminUserListDto>> Handle(
            GetUsersQuery request,
            CancellationToken cancellationToken)
        {
            var users = _userManager.Users;

            return await _responseDataService.CreateAsync(users, request.RequestData, cancellationToken);
        }
    }
}
