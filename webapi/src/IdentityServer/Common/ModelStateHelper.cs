using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Exceptions;

namespace NetClock.IdentityServer.Common
{
    // TODO: Temporal hasta que encuentre una manera de capturar los errores y devolver un ModelState.
    // o se cambie al manera de manejar los NetClock.Application.Common.Exceptions.ValidationException.
    public static class ModelStateHelper
    {
        public static void AddErrorsInModelState(
            ModelStateDictionary modelStateDictionary,
            ValidationException validationException)
        {
            foreach (var (key, value) in validationException.Failures)
            {
                foreach (var error in value)
                {
                    var keyError = string.Equals(key, Errors.NonFieldErrors, StringComparison.CurrentCultureIgnoreCase)
                        ? string.Empty
                        : key;

                    modelStateDictionary.AddModelError(keyError, error);
                }
            }
        }
    }
}
