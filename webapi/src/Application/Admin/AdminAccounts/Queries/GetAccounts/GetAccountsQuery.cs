using MediatR;
using NetClock.Application.Common.Http;

namespace NetClock.Application.Admin.AdminAccounts.Queries.GetAccounts
{
    public class GetAccountsQuery : IRequest<ResponseData<AdminAccountsListDto>>
    {
        public GetAccountsQuery(RequestData requestData)
        {
            RequestData = requestData;
        }

        public RequestData RequestData { get; }
    }
}
