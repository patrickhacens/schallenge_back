namespace SChallengeAPI.Models;

/// <summary>
/// Result of an authentication
/// </summary>
public class AuthResult
{
    /// <summary>
    /// JWT Access Token
    /// </summary>
    public string AccessToken { get; set; }
}
