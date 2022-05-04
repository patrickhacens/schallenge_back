using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Dashboard;

/// <summary>
/// Request Dashboard by users
/// </summary>
public class ByUserRequest : IRequest<ResultOf<DataC<UserEvents>>>
{
}
