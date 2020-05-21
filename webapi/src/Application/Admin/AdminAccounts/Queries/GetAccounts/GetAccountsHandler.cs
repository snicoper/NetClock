using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NetClock.Application.Common.Http;
using NetClock.Application.Common.Interfaces.Http;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Queries.GetAccounts
{
    public class GetAccountsHandler : IRequestHandler<GetAccountsQuery, ResponseData<AdminAccountsListDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IResponseDataService<ApplicationUser, AdminAccountsListDto> _responseDataService;

        public GetAccountsHandler(
            UserManager<ApplicationUser> userManager,
            IResponseDataService<ApplicationUser, AdminAccountsListDto> responseDataService)
        {
            _userManager = userManager;
            _responseDataService = responseDataService;
        }

        public async Task<ResponseData<AdminAccountsListDto>> Handle(
            GetAccountsQuery request,
            CancellationToken cancellationToken)
        {
            var users = _userManager.Users;

            return await _responseDataService.CreateAsync(users, request.RequestData, cancellationToken);
        }
    }
}
