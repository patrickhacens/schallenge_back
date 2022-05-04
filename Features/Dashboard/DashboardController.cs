using Microsoft.AspNetCore.Mvc;
using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Dashboard;

/// <summary>
/// Dashboard resource
/// </summary>
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    public DashboardController(IMediator mediator)
    {
        this.mediator=mediator;
    }

    /// <summary>
    /// Returns the users and the amount of events made by them
    /// </summary>
    [HttpGet("EventsByUser")]
    public Task<ResultOf<DataC<UserEvents>>> ByUser(CancellationToken cancellation)
        => mediator.Send(new ByUserRequest(), cancellation);

    /// <summary>
    /// Returns the top 10 events with more popularity
    /// </summary>
    [HttpGet("EventsPopularity")]
    public Task<ResultOf<DataC<EventDetail>>> Popularity(CancellationToken cancellation)
        => mediator.Send(new PopularityRequest(), cancellation);

    /// <summary>
    /// Returns the amount of event made in each month of the specified year
    /// </summary>
    [HttpGet("EventsByYear/{Year:int}")]
    public Task<ResultOf<DataC<EventMonthQuantity>>> ByYear([FromRoute] YearlyEventsRequest request, CancellationToken cancellation)
        => mediator.Send(request, cancellation);
}
