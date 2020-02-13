using System.Threading.Tasks;

namespace NetClock.Application.Common.Interfaces.Views
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync<TModel>(string viewName, TModel model);
    }
}
