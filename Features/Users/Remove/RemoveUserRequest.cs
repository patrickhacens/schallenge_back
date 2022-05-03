

namespace SChallengeAPI.Features.Users;

/// <summary>
/// Request to remove an user
/// </summary>
public class RemoveUserRequest : IRequest<Result>
{
    /// <summary>
    /// Id of user
    /// </summary>
    public Guid Id { get; set; }
}