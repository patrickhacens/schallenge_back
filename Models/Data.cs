namespace SChallengeAPI.Models;

/// <summary>
/// Collection of items
/// </summary>
/// <typeparam name="T"></typeparam>
public class DataC<T>
{
    /// <summary>
    /// Data
    /// </summary>
    public IEnumerable<T> Data { get; set; }
}
