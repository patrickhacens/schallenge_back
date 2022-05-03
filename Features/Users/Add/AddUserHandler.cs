using SChallengeAPI.Models;
using SChallengeAPI.Services;

namespace SChallengeAPI.Features.Users;

class AddUserHandler : IRequestHandler<AddUserRequest, ResultOf<RegisterResult>>
{
    private readonly Db db;
    private readonly PasswordHasher hasher;
    private readonly ILogger<AddUserHandler> logger;

    public AddUserHandler(Db db, PasswordHasher hasher, ILogger<AddUserHandler> logger)
    {
        this.db=db;
        this.hasher=hasher;
        this.logger=logger;
    }

    public async Task<ResultOf<RegisterResult>> Handle(AddUserRequest request, CancellationToken cancellationToken)
    {
        var (hash, salt) = hasher.Hash(request.Password);
        var user = request.Adapt<Domain.User>();
        user.Hash = hash;
        user.Salt = salt;

        db.Users.Add(user);
        await db.SaveChangesAsync(cancellationToken);

        logger.LogInformation("User with username '{username}' created", user.Username);
        return new RegisterResult(user.Id);
    }
}
