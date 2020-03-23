using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace NetClock.Application.Localization.Commands.SetCulture
{
    public class SetCultureHandler : IRequestHandler<SetCultureCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SetCultureHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<Unit> Handle(SetCultureCommand request, CancellationToken cancellationToken)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(request.Culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Unit.Task;
        }
    }
}
