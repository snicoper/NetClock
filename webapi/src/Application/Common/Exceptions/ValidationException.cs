using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using NetClock.Application.Common.Extensions;

namespace NetClock.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IReadOnlyCollection<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Errors.Add(propertyName.LowerCaseFirst(), propertyFailures);
            }
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
