namespace SChallengeAPI.Features.Events;

class RemoveEventValidator : AbstractValidator<RemoveEventRequest>
{
    public RemoveEventValidator()
    {
        RuleFor(d => d.Id).NotEmpty();
    }
}
