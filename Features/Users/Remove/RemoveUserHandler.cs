

namespace SChallengeAPI.Features.Users;

class RemoveUserHandler : IRequestHandler<RemoveUserRequest, Result>
{
    private readonly Db db;
    private readonly ILogger<RemoveUserHandler> logger;

    public RemoveUserHandler(Db db, ILogger<RemoveUserHandler> logger)
    {
        this.db=db;
        this.logger=logger;
    }

    public async Task<Result> Handle(RemoveUserRequest request, CancellationToken cancellationToken)
    {
        var user = await db.Users.SingleOrDefaultAsync(d => d.Id == request.Id, cancellationToken);
        if (user == null)
            return new NotFoundError(nameof(Domain.User), request.Id);

        db.Users.Remove(user);

        await db.SaveChangesAsync(cancellationToken);

        logger.LogInformation("User {id} has been removed", request.Id);

        return Result.Success;
    }
}
