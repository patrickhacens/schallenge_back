namespace SChallengeAPI.Domain;

/// <summary>
/// Represents an event
/// </summary>
public class Event
{
    /// <summary>
    /// Id of event
    /// </summary>
    public Guid Id { get; set; }

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
    /// Duration of the event
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Id of the user that created the event
    /// </summary>
    public Guid CreatorId { get; set; }

    /// <summary>
    /// User that created the event
    /// </summary>
    public User Creator { get; set; }
}
