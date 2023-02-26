namespace CalorieRestAPIMySQL.Models;

/// <summary>
/// Class representing a Calorie object with properties for its ID, Name, Quantity, Unit, and Value.
/// </summary>
public class CalorieClass
{
    /// <summary>
    /// The unique identifier for the Calorie object.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the food item.
    /// </summary>
    public string Name { get; set; } = String.Empty;

    /// <summary>
    /// The quantity of the food item.
    /// </summary>
    public int Quantity { get; set; } = 0;

    /// <summary>
    /// The unit of measurement for the food item.
    /// </summary>
    public string Unit { get; set; } = String.Empty;

    /// <summary>
    /// The number of calories in the food item.
    /// </summary>
    public int Calories { get; set; } = 0;
}
