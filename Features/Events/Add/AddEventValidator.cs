namespace SChallengeAPI.Features.Events;

class AddEventValidator : AbstractValidator<AddEventRequest>
{
    public AddEventValidator()
    {
        RuleFor(d => d.Name).NotEmpty();
        RuleFor(d => d.Participants).GreaterThanOrEqualTo(0);
        RuleFor(d => d.Date).NotEmpty();
        RuleFor(d => d.Duration).NotEmpty();
    }
}
