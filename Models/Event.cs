namespace SChallengeAPI.Models;

/// <summary>
/// Event
/// </summary>
public class Event
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Start of event
    /// </summary>
    public DateTime Date { get; set; }
}
