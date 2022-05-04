namespace SChallengeAPI.Features.Events;

class GetEventValidator : AbstractValidator<GetEventRequest>
{
    public GetEventValidator()
    {
        RuleFor(d => d.Id).NotEmpty();
    }
}
