namespace SChallengeAPI.Models;

/// <summary>
/// Month and quantity of events
/// </summary>
public class EventMonthQuantity
{
    /// <summary>
    /// First day of said month
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Month number (1-12)
    /// </summary>
    public int MonthNumber { get; set; }
    /// <summary>
    /// Month name
    /// </summary>
    public string Month { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    public int Quantity { get; set; }
}
