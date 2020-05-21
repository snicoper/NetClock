using MediatR;
using NetClock.Application.Common.Http;

namespace NetClock.Application.Admin.AdminRoles.Queries
{
    public class GetRolesQuery : IRequest<ResponseData<GetRolesDto>>
    {
        public GetRolesQuery(RequestData requestData)
        {
            RequestData = requestData;
        }

        public RequestData RequestData { get; }
    }
}
