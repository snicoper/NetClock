using FluentValidation;
using NetClock.Application.Common.Extensions;

namespace NetClock.Application.Localization.Commands.SetCulture
{
    public class SetCultureValidator : AbstractValidator<SetCultureCommand>
    {
        public SetCultureValidator()
        {
            RuleFor(v => v.Culture).NotEmpty().SupportedCulture();
        }
    }
}
