using System.Collections.Generic;

namespace NetClock.Application.Common.Interfaces.Validations
{
    public interface IValidationFailureService
    {
        /// <summary>
        /// Comprueba si tiene errores.
        /// </summary>
        /// <returns>true en caso de existir errores, false en caso contrario.</returns>
        bool HasErrors();

        /// <summary>
        /// Obtener el numero de errores.
        /// </summary>
        int ErrorsCount();

        /// <summary>
        /// Añadir un error.
        /// </summary>
        void Add(string key, string value);

        /// <summary>
        /// Añadir un diccionario con errores.
        /// </summary>
        void Add(Dictionary<string, string> errors);

        /// <summary>
        /// Añade un error y lanza la excepción con los errores.
        /// </summary>
        void AddAndRaiseException(string key, string value);

        /// <summary>
        /// Añade un diccionario con errores y lanza la excepción con los errores.
        /// </summary>
        void AddAndRaiseException(Dictionary<string, string> errors);

        /// <summary>
        /// Lanza la excepción tenga o no errores.
        /// </summary>
        void RaiseException();

        /// <summary>
        /// Lanza la excepción si tiene errores.
        /// </summary>
        void RaiseExceptionIfExistsErrors();
    }
}
