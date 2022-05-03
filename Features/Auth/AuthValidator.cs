namespace SChallengeAPI.Features.Auth;

class AuthValidator : AbstractValidator<AuthRequest>
{
    public AuthValidator()
    {
        RuleFor(d => d.Username).NotEmpty();
        RuleFor(d => d.Password).NotEmpty().MinimumLength(6);
    }
}
