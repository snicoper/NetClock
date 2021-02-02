using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NetClock.Application.Common.Http;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Queries.GetAccounts
{
    public class GetAccountsHandler : IRequestHandler<GetAccountsQuery, ResponseData<GetAccountsDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetAccountsHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResponseData<GetAccountsDto>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users;

            return await ResponseData<GetAccountsDto>.CreateAsync(users, request.RequestData, cancellationToken);
        }
    }
}
