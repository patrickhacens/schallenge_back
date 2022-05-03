using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Users;

/// <summary>
/// Reques to list all users, paginated
/// </summary>
public class ListUsersRequest : PageRequest, IRequest<ResultOf<PageResult<Reference>>>
{
}
