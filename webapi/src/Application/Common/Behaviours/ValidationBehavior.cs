using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using NetClock.Application.Common.Exceptions;

namespace NetClock.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);
            var validationResults =
                await Task.WhenAll(_validators.Select(val => val.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(val => val.Errors).Where(val => val is not null).ToList();

            if (failures.Any())
            {
                throw new CustomValidationException(failures);
            }

            return await next();
        }
    }
}
