using MediatR;

namespace NetClock.Application.Localization.Commands.SetCulture
{
    public class SetCultureCommand : IRequest
    {
        public SetCultureCommand(string culture)
        {
            Culture = culture;
        }

        public string Culture { get; }
    }
}
