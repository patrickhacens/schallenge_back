using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Auth;

/// <summary>
/// Request to authenticate
/// </summary>
public class AuthRequest : IRequest<ResultOf<AuthResult>>
{
    /// <summary>
    /// Username
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; }
}
