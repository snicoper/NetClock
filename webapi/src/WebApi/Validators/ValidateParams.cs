using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Interfaces.Validations;
using NetClock.Application.Common.Localizations;

namespace NetClock.WebApi.Validators
{
    public class ValidateParams : IValidateParams
    {
        private readonly IValidationFailureService _validationFailure;
        private readonly IStringLocalizer<SharedLocalizer> _localizer;
        private readonly ILogger<ValidateParams> _logger;

        public ValidateParams(
            IValidationFailureService validationFailure,
            IStringLocalizer<SharedLocalizer> localizer,
            ILogger<ValidateParams> logger)
        {
            _validationFailure = validationFailure;
            _localizer = localizer;
            _logger = logger;
        }

        public IValidateParams Equals<T>(T ida, T idb)
        {
            if (ida.Equals(idb))
            {
                return this;
            }

            _logger.LogWarning("Los parámetros {ida} y {idb} no parece iguales.", ida, idb);

            const string error = "Ha ocurrido un error inesperado, inténtelo en unos minutos.";
            _validationFailure.AddAndRaiseException(CommonErrors.NonFieldErrors, _localizer[error]);

            return this;
        }
    }
}
