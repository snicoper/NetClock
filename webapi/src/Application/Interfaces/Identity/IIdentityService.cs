using System.Threading.Tasks;
using NetClock.Application.Models;
using NetClock.Application.Models.Identity;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Interfaces.Identity
{
    public interface IIdentityService
    {
        Task<ApplicationUser> FirstOrDefaultBySlugAsync(string slug);

        Task<(Result Result, ApplicationUser ApplicationUser)> CreateUserAsync(IdentityUserCreate identityUserCreate);

        Task<Result> DeleteUserAsync(string userId);
    }
}
