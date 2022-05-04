using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Events;

class ListEventsHandler : IRequestHandler<ListEventsRequest, ResultOf<PageResult<Event>>>
{
    private readonly Db db;
    private readonly ILogger<ListEventsHandler> logger;

    public ListEventsHandler(Db db, ILogger<ListEventsHandler> logger)
    {
        this.db=db;
        this.logger=logger;
    }

    public async Task<ResultOf<PageResult<Event>>> Handle(ListEventsRequest request, CancellationToken cancellationToken)
    {
        var query = db.Events.AsQueryable();
        if (request.MinDate.HasValue)
            query = query.Where(d => d.Date >= request.MinDate);

        if (request.MaxDate.HasValue)
            query = query.Where(d => d.Date <= request.MaxDate);

        var total = await query.CountAsync(cancellationToken);
        var items = await query.PaginateBy(request, d => d.Name)
            .ProjectToType<Event>()
            .ToListAsync(cancellationToken);

        logger.LogInformation("Returning page {page} with {itemsCount} from a total of {total} events", request.Page, items.Count, total);

        return new PageResult<Event>(request, total, items);
    }
}
