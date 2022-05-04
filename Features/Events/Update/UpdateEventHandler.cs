namespace SChallengeAPI.Features.Events;

class UpdateEventHandler : IRequestHandler<UpdateEventRequest, Result>
{
    private readonly Db db;
    private readonly ILogger<UpdateEventHandler> logger;

    public UpdateEventHandler(Db db, ILogger<UpdateEventHandler> logger)
    {
        this.db=db;
        this.logger=logger;
    }

    public async Task<Result> Handle(UpdateEventRequest request, CancellationToken cancellationToken)
    {
        var _event = await db.Events
            .SingleOrDefaultAsync(d => d.Id == request.Id, cancellationToken);
        if (_event == null)
            return new NotFoundError(nameof(Domain.Event), request.Id);

        request.Information.Adapt(_event);

        await db.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated event {id}", request.Id);

        return Result.Success;
    }
}
