using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Dashboard;

/// <summary>
/// Request for EventMonthQuantity of a year
/// </summary>
public class YearlyEventsRequest : IRequest<ResultOf<DataC<EventMonthQuantity>>>
{
    /// <summary>
    /// Year that will be consulted
    /// </summary>
    public int Year { get; set; }
}

