using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Dashboard;

class ByUserHandler : IRequestHandler<ByUserRequest, ResultOf<DataC<UserEvents>>>
{
    private readonly Db db;
    private readonly ILogger<ByUserHandler> logger;

    public ByUserHandler(Db db, ILogger<ByUserHandler> logger)
    {
        this.db=db;
        this.logger=logger;
    }

    public async Task<ResultOf<DataC<UserEvents>>> Handle(ByUserRequest request, CancellationToken cancellationToken)
    {
        var result = await db.Events
            .GroupBy(d => d.CreatorId)
            .Select(d => new UserEvents
            {
                User = d.First().Creator.Adapt<Reference>(),
                Quantity = d.Count()
            })
            .ToListAsync(cancellationToken);

        logger.LogInformation("Returning dashboard method, events by user");

        return new DataC<UserEvents>
        {
            Data = result
        };
    }
}
