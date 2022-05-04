namespace SChallengeAPI.Features.Events;

class RemoveEventHandler : IRequestHandler<RemoveEventRequest, Result>
{
    private readonly Db db;
    private readonly ILogger<RemoveEventHandler> logger;

    public RemoveEventHandler(Db db, ILogger<RemoveEventHandler> logger)
    {
        this.db=db;
        this.logger=logger;
    }

    public async Task<Result> Handle(RemoveEventRequest request, CancellationToken cancellationToken)
    {
        var _event = await db.Events
            .AsNoTracking()
            .SingleOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (_event == null)
            return new NotFoundError(nameof(Domain.Event), request.Id);

        db.Events.Remove(_event);
        await db.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Event {eventid} has been removed", request.Id);

        return Result.Success;
    }
}
