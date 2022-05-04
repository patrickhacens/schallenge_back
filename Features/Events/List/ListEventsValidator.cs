namespace SChallengeAPI.Features.Events;

class ListEventsValidator : AbstractValidator<ListEventsRequest>
{
    public ListEventsValidator()
    {
        RuleFor(d => d.MinDate).LessThanOrEqualTo(d => d.MaxDate);
        RuleFor(d => d.MaxDate).GreaterThanOrEqualTo(d => d.MinDate);
    }
}
