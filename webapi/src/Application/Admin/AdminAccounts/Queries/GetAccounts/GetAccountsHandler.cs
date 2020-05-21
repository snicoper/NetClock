using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NetClock.Application.Common.Http;
using NetClock.Application.Common.Interfaces.Http;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Queries.GetAccounts
{
    public class GetAccountsHandler : IRequestHandler<GetAccountsQuery, ResponseData<GetAccountsDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IResponseDataService<ApplicationUser, GetAccountsDto> _responseDataService;

        public GetAccountsHandler(
            UserManager<ApplicationUser> userManager,
            IResponseDataService<ApplicationUser, GetAccountsDto> responseDataService)
        {
            _userManager = userManager;
            _responseDataService = responseDataService;
        }

        public async Task<ResponseData<GetAccountsDto>> Handle(
            GetAccountsQuery request,
            CancellationToken cancellationToken)
        {
            var users = _userManager.Users;

            return await _responseDataService.CreateAsync(users, request.RequestData, cancellationToken);
        }
    }
}
