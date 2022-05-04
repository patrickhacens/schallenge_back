using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Events;

/// <summary>
/// Events Resource
/// </summary>
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    public EventsController(IMediator mediator)
    {
        this.mediator=mediator;
    }

    /// <summary>
    /// List events, paginaged
    /// </summary>
    [HttpGet]
    public Task<ResultOf<PageResult<Event>>> List([FromQuery] ListEventsRequest request, CancellationToken cancellation)
        => mediator.Send(request, cancellation);

    /// <summary>
    /// Get event detail
    /// </summary>
    [HttpGet("{Id}")]
    public Task<ResultOf<EventDetail>> Get([FromRoute] GetEventRequest request, CancellationToken cancellation)
        => mediator.Send(request, cancellation);

    /// <summary>
    /// Create event, requires authentication
    /// </summary>
    [HttpPost]
    [Authorize]
    public Task<ResultOf<RegisterResult>> Post([FromBody] AddEventRequest request, CancellationToken cancellation)
        => mediator.Send(request, cancellation);

    /// <summary>
    /// Updates event, change all data even if missing
    /// </summary>
    [HttpPut("{id}")]
    public Task<Result> Update([FromRoute] Guid id, [FromBody] EventInformation information, CancellationToken cancellation)
        => mediator.Send(new UpdateEventRequest()
        {
            Id = id,
            Information = information,
        }, cancellation);

    /// <summary>
    /// Remove event
    /// </summary>
    [HttpDelete("{Id}")]
    public Task<Result> Delete([FromRoute] RemoveEventRequest request, CancellationToken cancellation)
        => mediator.Send(request, cancellation);
}
