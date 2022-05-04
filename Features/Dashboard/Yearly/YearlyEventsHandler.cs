using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Dashboard;

class YearlyEventsHandler : IRequestHandler<YearlyEventsRequest, ResultOf<DataC<EventMonthQuantity>>>
{
    private readonly Db db;
    private readonly ILogger<YearlyEventsHandler> logger;

    public YearlyEventsHandler(Db db, ILogger<YearlyEventsHandler> logger)
    {
        this.db=db;
        this.logger=logger;
    }

    private static readonly Dictionary<int, string> monthTranslations = new Dictionary<int, string>()
    {
        { 1, "January" },
        { 2, "February" },
        { 3, "March" },
        { 4, "April" },
        { 5, "May" },
        { 6, "June" },
        { 7, "July" },
        { 8, "August" },
        { 9, "September" },
        { 10, "October" },
        { 11, "November" },
        { 12, "December" }
    };

    public async Task<ResultOf<DataC<EventMonthQuantity>>> Handle(YearlyEventsRequest request, CancellationToken cancellationToken)
    {
        var result = await db.Events
            .Where(d => d.Date.Year == request.Year)
            .GroupBy(d => d.Date.Month)
            .Select(d => new EventMonthQuantity
            {
                Date = new DateTime(request.Year, d.Key, 1),
                MonthNumber = d.Key,
                Quantity = d.Count()
            })
            .OrderBy(d => d.MonthNumber)
            .ToListAsync(cancellationToken);

        foreach (var item in result)
            item.Month = monthTranslations[item.MonthNumber];

        logger.LogInformation("Returning dashboard method, yearly events for year {year}", request.Year);

        return new DataC<EventMonthQuantity>
        {
            Data = result
        };
    }
}

