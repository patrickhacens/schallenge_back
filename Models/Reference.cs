namespace SChallengeAPI.Models;

/// <summary>
/// Reference of some entity, this contain only the minimum ammount of information
/// </summary>
public class Reference
{
    /// <summary>
    /// Id of reference
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Human friendly id of reference
    /// </summary>
    public string Name { get; set; }
}
