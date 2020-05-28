using System.Threading.Tasks;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Common.Interfaces.Identity
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<ApplicationUser> FirstOrDefaultBySlugAsync(string slug);

        Task<ApplicationUser> CreateUser(ApplicationUser applicationUser, string password);
    }
}
