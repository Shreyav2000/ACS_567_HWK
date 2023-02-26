using CalorieRestAPIMySQL.Models;
using CalorieRestAPIMySQL.Data;

using System.Collections.Generic;
using System.IO;

/// <summary>
/// The `Seed` class is used to seed data into a database.
/// </summary>
public class Seed
{
    /// <summary>
    /// A reference to the database context.
    /// </summary>

    private readonly DataContext dataContext;

    /// <summary>
    /// Initializes a new instance of the `Seed` class.
    /// </summary>
    /// <param name="dataContext">A reference to the database context.</param>
    public Seed(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    /// <summary>
    /// Seeds data into the database.
    /// </summary>
    public void SeedDataContext()
    {
        if (!dataContext.Calorie.Any())
        {
            List<CalorieClass> calories = ReadCaloriesFromFile("calorie.csv");
            dataContext.Calorie.AddRange(calories);
            dataContext.SaveChanges();
        }
    }

    /// <summary>
    /// Reads data from a CSV file and returns a list of `Calorie` objects.
    /// </summary>
    /// <param name="filePath">The path to the CSV file.</param>
    /// <returns>A list of `Calorie` objects.</returns>
    private List<CalorieClass> ReadCaloriesFromFile(string filePath)
    {
        List<CalorieClass> calories = new List<CalorieClass>();

        using (var reader = new StreamReader(filePath))
        {
            // skip the header row
            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                var calorie = new CalorieClass
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                    Quantity = int.Parse(values[2]),
                    Unit = values[3],
                    Calories = int.Parse(values[4])
                };

                calories.Add(calorie);
            }
        }

        return calories;
    }
}

