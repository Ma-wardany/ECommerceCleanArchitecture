using FluentValidation;
using MediatR;

namespace E_COMMERCE_APP.Core.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>
         : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(vr => vr.ValidateAsync(context, cancellationToken)));

                if(validationResults.Any())
                {
                    var failures = validationResults.SelectMany(f => f.Errors).Where(f => f != null).ToList();

                    if(failures.Count != 0)
                    {
                        var errorMessages = string.Join("; ", failures.Select(f => f.ErrorMessage));
                        
                        throw new ValidationException(errorMessages.ToString());
                    }
                }
            }

            return await next();
        }
    }
}
