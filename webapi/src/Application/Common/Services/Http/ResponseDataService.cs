using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using NetClock.Application.Common.Http;
using NetClock.Application.Common.Interfaces.Http;

namespace NetClock.Application.Common.Services.Http
{
    public class ResponseDataService<TQueryModel, TResultViewModel>
        : IResponseDataService<TQueryModel, TResultViewModel>
        where TQueryModel : class
        where TResultViewModel : class
    {
        private readonly IMapper _mapper;

        public ResponseDataService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ResponseData<TResultViewModel>> CreateAsync(
            IQueryable<TQueryModel> source,
            RequestData request,
            CancellationToken cancellationToken)
        {
            return await ResponseData<TResultViewModel>.CreateAsync(source, request, _mapper, cancellationToken);
        }
    }
}
