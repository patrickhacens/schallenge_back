namespace SChallengeAPI.Features.Events;

/// <summary>
/// Request to update an event
/// </summary>
public class UpdateEventRequest : IRequest<Result>
{
    /// <summary>
    /// Event id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Event's new information
    /// </summary>
    public EventInformation Information { get; set; }
}

/// <summary>
/// Event information to update
/// </summary>
public class EventInformation
{
    /// <summary>
    /// Name of the event
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Number of participants
    /// </summary>
    public int Participants { get; set; }

    /// <summary>
    /// Date of the start of the event
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Duration of the event (00:00:00)
    /// </summary>
    public TimeSpan Duration { get; set; }
}
