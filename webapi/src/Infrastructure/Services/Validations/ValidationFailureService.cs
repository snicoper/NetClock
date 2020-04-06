using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using NetClock.Application.Common.Exceptions;
using NetClock.Application.Common.Interfaces.Validations;

namespace NetClock.Infrastructure.Services.Validations
{
    public class ValidationFailureService : IValidationFailureService
    {
        public ValidationFailureService()
        {
            Errors = new List<ValidationFailure>();
        }

        private List<ValidationFailure> Errors { get; }

        public bool HasErrors()
        {
            return Errors.Any();
        }

        public int ErrorsCount()
        {
            return Errors.Count;
        }

        public void Add(string property, string error)
        {
            Errors.Add(new ValidationFailure(property, error));
        }

        public void Add(Dictionary<string, string> errors)
        {
            foreach (var (key, value) in errors)
            {
                Add(key, value);
            }
        }

        public void AddAndRaiseException(string key, string value)
        {
            Add(key, value);
            RaiseException();
        }

        public void AddAndRaiseException(Dictionary<string, string> errors)
        {
            foreach (var (key, value) in errors)
            {
                Add(key, value);
            }

            RaiseException();
        }

        public void RaiseException()
        {
            throw new ValidationException(Errors);
        }

        public void RaiseExceptionIfExistsErrors()
        {
            if (HasErrors())
            {
                throw new ValidationException(Errors);
            }
        }
    }
}
