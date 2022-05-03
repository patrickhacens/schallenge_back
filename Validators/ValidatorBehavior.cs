namespace SChallengeAPI.Validators;

class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class, Nudes.Retornator.Core.IResult
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators=validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var validationTask = validators.Select(d =>
            d.ValidateAsync(request, cancellationToken));

        await Task.WhenAll(validationTask);

        var errors = validationTask
            .Select(d => d.Result)
            .Where(d => !d.IsValid)
            .SelectMany(d => d.Errors)
            .GroupBy(d => d.PropertyName)
            .ToDictionary(d => d.Key, d => (ICollection<string>)d.Select(d => d.ErrorMessage).ToList());

        if (errors.Any())
        {
            FieldErrors errs = new(errors);
            var result = Activator.CreateInstance<TResponse>();
            result.Error = new BadRequestError()
            {
                FieldErrors = errs
            };
            return result;
        }
        return await next();
    }
}
