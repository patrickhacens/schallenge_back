namespace SChallengeAPI.Features.Users;

class AddUserValidator : AbstractValidator<AddUserRequest>
{
    public AddUserValidator(UserUniqueValidator<AddUserRequest> validator)
    {
        RuleFor(d => d.Username)
            .NotEmpty()
            .SetAsyncValidator(validator);
        RuleFor(d => d.Password).MinimumLength(6);
    }
}
