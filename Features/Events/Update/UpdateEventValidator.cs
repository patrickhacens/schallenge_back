namespace SChallengeAPI.Features.Events;

class UpdateEventValidator : AbstractValidator<UpdateEventRequest>
{
    public UpdateEventValidator()
    {
        RuleFor(d => d.Id).NotEmpty();
        RuleFor(d => d.Information).NotNull();

        RuleFor(d => d.Information.Name).NotEmpty();
        RuleFor(d => d.Information.Duration).NotEmpty();
        RuleFor(d => d.Information.Date).NotEmpty();
        RuleFor(d => d.Information.Participants).GreaterThan(0);
        RuleFor(d => d.Information.Duration).NotEmpty();
    }
}
