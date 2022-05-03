using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Users;

/// <summary>
/// Request to add an user
/// </summary>
public class AddUserRequest : IRequest<ResultOf<RegisterResult>>
{
    /// <summary>
    /// Username, used for authentication
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Password, used for authentication
    /// The password will be hashed and sanitized on the persistence layer
    /// </summary>
    public string Password { get; set; }
}
