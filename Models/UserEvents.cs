namespace SChallengeAPI.Models;

/// <summary>
/// Tuple of user and quantity of events created by him
/// </summary>
public class UserEvents
{
    /// <summary>
    /// User
    /// </summary>
    public Reference User { get; set; }

    /// <summary>
    /// Quantity of events made by user
    /// </summary>
    public int Quantity { get; set; }
}