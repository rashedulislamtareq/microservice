using FluentValidation;
using MediatR;
using ValidationException = Ordering.Application.Exceptions.ValidationException;

namespace Ordering.Application.Behaviours;

public class ValidationBehaviour<TReq, TRes> : IPipelineBehavior<TReq, TRes>
{
    private readonly IEnumerable<IValidator<TReq>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TReq>> validators)
    {
        _validators = validators;
    }

    public async Task<TRes> Handle(TReq request, RequestHandlerDelegate<TRes> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TReq>(request);
            var validationResults = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context, cancellationToken)));

            var failures = validationResults.SelectMany(x=>x.Errors).Where(y=>y != null).ToList();

            if (failures.Count != 0) 
                throw new ValidationException(failures);
        }

        return await next();

    }
}
