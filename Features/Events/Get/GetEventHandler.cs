using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Events;

class GetEventHandler : IRequestHandler<GetEventRequest, ResultOf<EventDetail>>
{
    private readonly Db db;
    private readonly ILogger<GetEventHandler> logger;

    public GetEventHandler(Db db, ILogger<GetEventHandler> logger)
    {
        this.db=db;
        this.logger=logger;
    }

    public async Task<ResultOf<EventDetail>> Handle(GetEventRequest request, CancellationToken cancellationToken)
    {
        var _event = await db.Events
            .AsNoTracking()
            .ProjectToType<EventDetail>()
            .SingleOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (_event == null)
            return new NotFoundError(nameof(Domain.Event), request.Id);

        logger.LogInformation("Returning details of event {eventId}", request.Id);

        return _event;
    }
}
