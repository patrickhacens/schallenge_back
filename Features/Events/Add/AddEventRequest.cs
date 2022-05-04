using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Events;

/// <summary>
/// Request to add an event
/// </summary>
public class AddEventRequest : IRequest<ResultOf<RegisterResult>>
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
    /// Duration of the event in the format "00:00:00"
    /// </summary>
    public TimeSpan Duration { get; set; }
}
