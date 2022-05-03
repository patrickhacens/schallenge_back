using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Users;

class ListUsersHandler : IRequestHandler<ListUsersRequest, ResultOf<PageResult<Reference>>>
{
    private readonly Db db;
    private readonly ILogger<ListUsersHandler> logger;

    public ListUsersHandler(Db db, ILogger<ListUsersHandler> logger)
    {
        this.db=db;
        this.logger=logger;
    }

    public async Task<ResultOf<PageResult<Reference>>> Handle(ListUsersRequest request, CancellationToken cancellationToken)
    {
        var query = db.Users.AsNoTracking().AsQueryable();

        var total = await query.CountAsync(cancellationToken);

        var items = await query
            .PaginateBy(request, d => d.Username)
            .ProjectToType<Reference>()
            .ToListAsync(cancellationToken);

        logger.LogInformation("Returning page {page} with {itemsCount} from a total of {total} users", request.Page, items.Count, total);

        return new PageResult<Reference>(request, total, items);
    }
}
