using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Dashboard;

/// <summary>
/// Request events by popularity
/// </summary>
public class PopularityRequest: IRequest<ResultOf<DataC<EventDetail>>>
{
}
