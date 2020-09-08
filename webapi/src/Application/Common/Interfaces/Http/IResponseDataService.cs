using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetClock.Application.Common.Http;

namespace NetClock.Application.Common.Interfaces.Http
{
    /// <summary>
    /// Wrapper para inyectar IMapper a la paginación.
    /// </summary>
    /// <typeparam name="TQueryModel">Resultado IQueryable.</typeparam>
    /// <typeparam name="TResultDto">Tipo a devolver en ResponseData.Items.</typeparam>
    public interface IResponseDataService<in TQueryModel, TResultDto>
        where TQueryModel : class
        where TResultDto : class
    {
        /// <summary>
        /// Wrapper de ResponseData.CreateAsync().
        /// </summary>
        /// <param name="source">IQueryable.</param>
        /// <param name="request">RequestData original.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Un ResponseData con el resultado.</returns>
        Task<ResponseData<TResultDto>> CreateAsync(
            IQueryable<TQueryModel> source,
            RequestData request,
            CancellationToken cancellationToken);
    }
}
