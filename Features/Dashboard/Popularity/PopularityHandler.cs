using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Dashboard;

class PopularityHandler : IRequestHandler<PopularityRequest, ResultOf<DataC<EventDetail>>>
{
    private readonly Db db;
    private readonly ILogger<PopularityHandler> logger;

    public PopularityHandler(Db db, ILogger<PopularityHandler> logger)
    {
        this.db=db;
        this.logger=logger;
    }

    public async Task<ResultOf<DataC<EventDetail>>> Handle(PopularityRequest request, CancellationToken cancellationToken)
    {
        var result = await db.Events
            .OrderByDescending(d => d.Participants)
            .Take(10)
            .ProjectToType<EventDetail>()
            .ToListAsync(cancellationToken);

        logger.LogInformation("Returning dashboard method, top events more popular");

        return new DataC<EventDetail>
        {
            Data = result
        };
    }
}
