
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using static Program;
using System.Drawing;

class Program
{
    public class Calorie
    {
        public string? Food { get; set; }
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public int Calories { get; set; }
    }

    public static List<Calorie> calories { get; set; } = new List<Calorie>();

    static void Main(string[] args)
    {
        // Main loop that runs until the user chooses to exit

        while (true)
        {
            Console.WriteLine("1. Read data from file");
            Console.WriteLine("2. Add data to file");
            Console.WriteLine("3. Analyze data");
            Console.WriteLine("4. Filter data by food name");
            Console.WriteLine("5. Filter data by calories");
            Console.WriteLine("6. Exit");
            Console.WriteLine("Enter a number to select an option:");

            int choice;
            // Get user input and validate it
            if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 6)
            {
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    ReadDataFromFile();
                    break;
                case 2:
                    AddData();
                    break;
                case 3:
                    AnalyzeData();
                    break;
                case 4:
                    FilterDataByFood();
                    break;
                case 5:
                    FilterDataByCalories();
                    break;
                case 6:
                    Exit();
                    break;

            }
        }
    }

    // Function to read data from file
    private static void ReadDataFromFile()
    {
        // File path
        var filePath = "../../../calorie.csv";
        try
        {
            // Check if file path is provided
            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("No file path provided!");
                return;
            }
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found!");
                return;
            }
            // Use StreamReader to read file
            using (var reader = new StreamReader(filePath))

            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        var parts = line.Split(',');
                        if (parts != null && parts.Length >= 4)
                        {
                            var calorie = new Calorie
                            {
                                Food = parts[0],
                                Quantity = int.TryParse(parts[1], out int q) ? q : default(int),
                                Unit = parts[2],
                                Calories = int.TryParse(parts[3], out int c) ? c : default(int)
                            };
                            calories.Add(calorie);
                        }
                    }
                }
            }

            Console.WriteLine("Data read from file successfully.");
        }
        catch (IOException)
        {
            Console.WriteLine("An error occurred while reading the file.");
        }
    }


    // Add data to file
    private static void AddData()
    {
        // Get food name from user
        Console.Write("Enter food: ");
        var food = Console.ReadLine();

        // Get quantity from user
        Console.Write("Enter quantity: ");
        int quantity;
        if (!int.TryParse(Console.ReadLine(), out quantity))
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        // Get unit from user
        Console.Write("Enter unit: ");
        var unit = Console.ReadLine();

        // Get calories from user
        Console.Write("Enter calories: ");
        int caloriesCount;
        if (!int.TryParse(Console.ReadLine(), out caloriesCount))
        {
            Console.WriteLine("Invalid calories.");
            return;
        }

        // Create a new calorie object with the user input
        var newCalorie = new Calorie { Food = food, Quantity = quantity, Unit = unit, Calories = caloriesCount };

        try
        {
            // Path of file to save the data to
            var filePath = "../../../calorie.csv";
            // If the file does not exist, create it
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            // Open the file and append the new calorie data
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.Write(newCalorie.Food + ",");
                sw.Write(newCalorie.Quantity + ",");
                sw.Write(newCalorie.Unit + ",");
                sw.Write(newCalorie.Calories + ",");
                sw.WriteLine();
                // Print success message
                Console.WriteLine("Data added to file successfully.");
            }
        }
        catch (IOException)
        {
            Console.WriteLine("An error occurred while writing to the file.");
        }
    }


    private static void AnalyzeData()
    {
        // check if the list is empty
        if (calories.Count() == 0)
        {
            Console.WriteLine("The list is empty. No data to analyze.");
            return;
        }

        // calculate the mean of calories


        double meanCalories = calories
            .Select(a => a.Calories)
            .Sum() / calories.Count();


        // calculate the median of calories
        var caloriesList = calories.Select(c => (double)c.Calories).ToList();
        caloriesList.Sort();
        double medianCalories = FindMedian(caloriesList);

        // calculate minimum and maximum calories
        var minCalories = calories.Min(c => c.Calories);
        var maxCalories = calories.Max(c => c.Calories);

        Console.WriteLine("Mean calories: " + meanCalories);
        Console.WriteLine("Median calories: " + medianCalories);
        Console.WriteLine("Minimum calories: " + minCalories);
        Console.WriteLine("Maximum calories: " + maxCalories);
    }

    private static double FindMedian(List<double> caloriesList)
    {
        // check if the list is empty
        if (caloriesList.Count == 0)
        {
            return 0;
        }

        if (caloriesList.Count % 2 == 0)
        {
            // If the list has an even number of elements, return the average of the middle two elements
            int middleIndex1 = caloriesList.Count / 2 - 1;
            int middleIndex2 = middleIndex1 + 1;
            return (caloriesList[middleIndex1] + caloriesList[middleIndex2]) / 2;
        }
        else
        {
            // If the list has an odd number of elements, return the middle element
            int middleIndex = (caloriesList.Count - 1) / 2;
            return caloriesList[middleIndex];
        }

    }
    // FilterDataByFood method takes user input for a food name and filters the list of calories to show only the entries with a matching food name
    private static void FilterDataByFood()
    {
        Console.Write("Enter food name: ");
        var foodName = Console.ReadLine();

        if (string.IsNullOrEmpty(foodName))
        {
            Console.WriteLine("Food name cannot be empty.");
            return;
        }
        // Use LINQ to filter the list of calories by the food nam
        var filteredCalories = calories.Where(c => string.Compare(c.Food, foodName, true) == 0).ToList();

        if (filteredCalories.Count == 0)
        {
            Console.WriteLine("No data found for the given food name.");
        }
        else
        {
            Console.WriteLine("Filtered data:");
            foreach (var calorie in filteredCalories)
            {
                Console.WriteLine("Food: {0}, Quantity: {1}, Unit: {2}, Calories: {3}", calorie.Food, calorie.Quantity, calorie.Unit, calorie.Calories);
            }
        }
    }


    // FilterDataByCalories method takes user input for number of calories and filters the list of calories to show only the entries with matching number of calories
    private static void FilterDataByCalories()
    {
        // Check if the list of calories is not null or empty
        if (calories == null || calories.Count == 0)
        {
            Console.WriteLine("No data found.");
            return;
        }
        Console.Write("Enter the number of calories: ");
        var input = Console.ReadLine();
        int numberOfCalories;
        // Check if the input can be parsed as an integer

        if (!int.TryParse(input, out numberOfCalories))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }

        // Use LINQ to filter the list of calories by the number of calories
        var filteredCalories = calories.Where(c => c.Calories == numberOfCalories).ToList();

        if (filteredCalories.Count == 0)
        {
            Console.WriteLine("No data found for the given number of calories.");
        }
        else
        {
            Console.WriteLine("Filtered data:");
            foreach (var calorie in filteredCalories)
            {
                Console.WriteLine("Food: {0}, Quantity: {1}, Unit: {2}, Calories: {3}", calorie.Food, calorie.Quantity, calorie.Unit, calorie.Calories);
            }
        }
    }


    private static void Exit()
    {

        Console.WriteLine("Exiting program...");
        Environment.Exit(0);
    }
}

