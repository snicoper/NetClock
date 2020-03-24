using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Commands.EditUser
{
    public class EditUserHandler : IRequestHandler<EditUserCommand, ApplicationUser>
    {
        public Task<ApplicationUser> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
