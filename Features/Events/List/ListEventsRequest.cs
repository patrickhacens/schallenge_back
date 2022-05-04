using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Events;

/// <summary>
/// Request to list all events, paginated
/// </summary>
public class ListEventsRequest : PageRequest, IRequest<ResultOf<PageResult<Event>>>
{
    /// <summary>
    /// Minimal date
    /// </summary>
    public DateTime? MinDate { get; set; }

    /// <summary>
    /// Maximum date
    /// </summary>
    public DateTime? MaxDate { get; set; }

}