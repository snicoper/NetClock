using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using NetClock.Application.Common.Http;
using NetClock.Application.Common.Interfaces.Http;

namespace NetClock.Application.Common.Services.Http
{
    public class ResponseDataService<TQueryModel, TResultDto> : IResponseDataService<TQueryModel, TResultDto>
        where TQueryModel : class
        where TResultDto : class
    {
        private readonly IMapper _mapper;

        public ResponseDataService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ResponseData<TResultDto>> CreateAsync(
            IQueryable<TQueryModel> source,
            RequestData request,
            CancellationToken cancellationToken)
        {
            return await ResponseData<TResultDto>.CreateAsync(source, request, _mapper, cancellationToken);
        }
    }
}
