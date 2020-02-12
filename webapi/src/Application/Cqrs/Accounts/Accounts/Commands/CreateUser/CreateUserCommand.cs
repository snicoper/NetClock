using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NetClock.Application.Constants;
using NetClock.Application.Interfaces.Identity;
using NetClock.Application.Interfaces.Validations;
using NetClock.Application.Models.Identity;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Cqrs.Accounts.Accounts.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<ApplicationUser>
    {
        public CreateUserCommand(string userName, string email, string password)
        {
            UserName = userName;
            Email = email;
            Password = password;
        }

        [Display(Name = "Nombre de usuario")]
        public string UserName { get; }

        [Display(Name = "Correo electrónico")]
        public string Email { get; }

        [Display(Name = "Contraseña")]
        public string Password { get; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApplicationUser>
        {
            private readonly IValidationFailureService _validationFailureService;
            private readonly IIdentityService _identityService;
            private readonly IMapper _mapper;

            public CreateUserCommandHandler(
                IValidationFailureService validationFailureService,
                IIdentityService identityService,
                IMapper mapper)
            {
                _validationFailureService = validationFailureService;
                _identityService = identityService;
                _mapper = mapper;
            }

            public async Task<ApplicationUser> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var (result, applicationUser) =
                    await _identityService.CreateUserAsync(_mapper.Map<IdentityUserCreate>(request));

                if (result.Succeeded)
                {
                    return applicationUser;
                }

                foreach (var error in result.Errors)
                {
                    _validationFailureService.Add(Errors.NonFieldErrors, error);
                }

                _validationFailureService.RaiseExceptionsIfExistsFailures();

                return applicationUser;
            }
        }
    }
}
