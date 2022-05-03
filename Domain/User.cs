namespace SChallengeAPI.Domain;

/// <summary>
/// User
/// </summary>
public class User
{
    /// <summary>
    /// Id of user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Username
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Hashed password
    /// </summary>
    public string Hash { get; set; }

    /// <summary>
    /// Salt of hashed password
    /// </summary>
    public string Salt { get; set; }

    /// <summary>
    /// Events that this users created
    /// </summary>
    public ICollection<Event> Events { get; set; }
}
