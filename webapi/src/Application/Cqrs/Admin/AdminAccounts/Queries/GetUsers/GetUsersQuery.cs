using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NetClock.Application.Interfaces.Http;
using NetClock.Application.Models.Http;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Cqrs.Admin.AdminAccounts.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<ResponseData<AdminUserListViewModel>>
    {
        public GetUsersQuery(RequestData requestData)
        {
            RequestData = requestData;
        }

        private RequestData RequestData { get; }

        public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ResponseData<AdminUserListViewModel>>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IResponseDataService<ApplicationUser, AdminUserListViewModel> _responseDataService;

            public GetUsersQueryHandler(
                UserManager<ApplicationUser> userManager,
                IResponseDataService<ApplicationUser, AdminUserListViewModel> responseDataService)
            {
                _userManager = userManager;
                _responseDataService = responseDataService;
            }

            public async Task<ResponseData<AdminUserListViewModel>> Handle(
                GetUsersQuery request,
                CancellationToken cancellationToken)
            {
                var users = _userManager.Users;

                return await _responseDataService.CreateAsync(users, request.RequestData, cancellationToken);
            }
        }
    }
}
