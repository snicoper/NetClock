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
            Failures = new List<ValidationFailure>();
        }

        private List<ValidationFailure> Failures { get; }

        public bool HasErrors()
        {
            return Failures.Any();
        }

        public int ErrorsCount()
        {
            return Failures.Count;
        }

        public void Add(string property, string error)
        {
            Failures.Add(new ValidationFailure(property, error));
        }

        public void Add(Dictionary<string, string> errors)
        {
            foreach (var (key, value) in errors)
            {
                Add(key, value);
            }
        }

        public void AddAndRaiseExceptions(string key, string value)
        {
            Add(key, value);
            RaiseExceptions();
        }

        public void AddAndRaiseExceptions(Dictionary<string, string> errors)
        {
            foreach (var (key, value) in errors)
            {
                Add(key, value);
            }

            RaiseExceptions();
        }

        public void RaiseExceptions()
        {
            throw new ValidationException(Failures);
        }

        public void RaiseExceptionsIfExistsFailures()
        {
            if (HasErrors())
            {
                throw new ValidationException(Failures);
            }
        }
    }
}
