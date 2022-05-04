namespace SChallengeAPI.Features.Events;

/// <summary>
/// Request to remove an event
/// </summary>
public class RemoveEventRequest : IRequest<Result>
{
    /// <summary>
    /// Event id
    /// </summary>
    public Guid Id { get; set; }
}