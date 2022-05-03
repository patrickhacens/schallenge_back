using FluentValidation.Validators;

namespace SChallengeAPI.Validators;

class UserUniqueValidator<T> : IAsyncPropertyValidator<T, string>
{
    private readonly Db db;

    public UserUniqueValidator(Db db)
    {
        this.db=db;
    }

    public string Name => "User already exists in database";

    public string GetDefaultMessageTemplate(string errorCode) => Name;

    public async Task<bool> IsValidAsync(ValidationContext<T> context, string value, CancellationToken cancellation)
    {
        return !await db.Users.AnyAsync(d => d.Username == value, cancellation);
    }
}
