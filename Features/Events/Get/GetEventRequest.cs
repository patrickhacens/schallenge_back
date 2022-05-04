using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Events;

/// <summary>
/// Request to get details of an event
/// </summary>
public class GetEventRequest : IRequest<ResultOf<EventDetail>>
{
    /// <summary>
    /// Event id
    /// </summary>
    public Guid Id { get; set; }
}
