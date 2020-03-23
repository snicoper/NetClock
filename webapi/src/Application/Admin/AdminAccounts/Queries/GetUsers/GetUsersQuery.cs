using MediatR;
using NetClock.Application.Common.Http;

namespace NetClock.Application.Admin.AdminAccounts.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<ResponseData<AdminUserListViewModel>>
    {
        public GetUsersQuery(RequestData requestData)
        {
            RequestData = requestData;
        }

        public RequestData RequestData { get; }
    }
}
