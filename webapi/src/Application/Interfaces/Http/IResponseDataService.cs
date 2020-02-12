using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetClock.Application.Models.Http;

namespace NetClock.Application.Interfaces.Http
{
    /// <summary>
    /// Wrapper para inyectar IMapper a la paginación.
    /// </summary>
    /// <typeparam name="TQueryModel">Resultado IQueryable.</typeparam>
    /// <typeparam name="TResultViewModel">Tipo a devolver en ResponseData.Items.</typeparam>
    public interface IResponseDataService<in TQueryModel, TResultViewModel>
        where TQueryModel : class
        where TResultViewModel : class
    {
        /// <summary>
        /// Wrapper de ResponseData.CreateAsync().
        /// </summary>
        /// <param name="source">IQueryable.</param>
        /// <param name="request">RequestData original.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Un ResponseData con el resultado.</returns>
        Task<ResponseData<TResultViewModel>> CreateAsync(
            IQueryable<TQueryModel> source,
            RequestData request,
            CancellationToken cancellationToken);
    }
}
