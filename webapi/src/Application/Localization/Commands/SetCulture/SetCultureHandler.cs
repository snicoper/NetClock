using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Interfaces.Validations;
using NetClock.Application.Common.Localizations;

namespace NetClock.Application.Localization.Commands.SetCulture
{
    public class SetCultureHandler : IRequestHandler<SetCultureCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<CultureLocalizer> _localizer;
        private readonly IValidationFailureService _validationFailure;

        public SetCultureHandler(
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<CultureLocalizer> localizer,
            IValidationFailureService validationFailure)
        {
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            _validationFailure = validationFailure;
        }

        public Task<Unit> Handle(SetCultureCommand request, CancellationToken cancellationToken)
        {
            if (Cultures.Supported.FirstOrDefault(c => c.Name == request.Culture) is null)
            {
                var message = _localizer["El lenguaje que intenta cambiar no esta soportado."];
                _validationFailure.AddAndRaiseException(nameof(SetCultureCommand.Culture), message);
            }

            _httpContextAccessor.HttpContext?.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(request.Culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return Unit.Task;
        }
    }
}
