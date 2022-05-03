namespace SChallengeAPI.Models;

/// <summary>
/// Result of a registration
/// </summary>
public class RegisterResult
{
    /// <summary>
    /// Empty contructor
    /// </summary>
    public RegisterResult()
    {
    }

    /// <summary>
    /// Id constructor
    /// </summary>
    public RegisterResult(Guid id)
    {
        Id=id;
    }

    /// <summary>
    /// Id of resource
    /// </summary>
    public Guid Id { get; set; }
}
