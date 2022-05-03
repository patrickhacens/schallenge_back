

namespace SChallengeAPI.Features.Users;

class RemoveUserValidator : AbstractValidator<RemoveUserRequest>
{
    public RemoveUserValidator()
    {
        RuleFor(d => d.Id).NotEmpty();
    }
}
